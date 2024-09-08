using PBASE.Entity;
using PBASE.Entity.Enum;
using PBASE.Service;
using System;
using System.Web.Http;
using System.Web.Http.Description;
using PBASE.WebAPI.Controllers;
using PBASE.WebAPI.ViewModels;
using System.Threading.Tasks;
using PBASE.WebAPI;
using System.Net.Http;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Globalization;
using System.Web.Http.ModelBinding;
using PBASE.WebAPI.Helpers;
using Probase.GridHelper;
using PBASE.Repository.Infrastructure;

namespace PBASE.WebAPI.Controllers
{
    [MenuPermission(Menus.LoginAnalysis)]
    public class LoginAnalysisController : BaseController
    {
        #region Initialization

        private readonly ILookupService lookupService;
        private readonly IUserService userService;
        private ApplicationUserManager _userManager = null;

        public LoginAnalysisController(ILookupService lookupService, IUserService userService
        )
        {
            this.lookupService = lookupService;
            this.userService = userService;
        }

        public LoginAnalysisController(ApplicationUserManager userManager
            )
        {
            UserManager = userManager;
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        #endregion Initialization


        #region LoginAnalysis

        [HttpPost]
        [RequestValidation]
        [ReadOnlyValidation(Menus.LoginAnalysis)]
        [Route("api/loginAnalysis")]
        public async Task<IHttpActionResult> PutLoginAnalysis_LoginAnalysisForm(LoginAnalysis_LoginAnalysisFormViewModel model)
        {
            List<LoginAnalysis> items = new List<LoginAnalysis>();
            if (model.Id.HasValue && model.Id.Value > 0)
            {
                try
                {
                    DateTime? startDate = null;
                    DateTime? endDate = null;
                    IEnumerable<vw_AspNetUserAccountLogs> logs = null;
                    if (model.StartDate != null)
                    {
                        startDate = DateTime.ParseExact(model.StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    if (model.EndDate != null)
                    {
                        endDate = DateTime.ParseExact(model.EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    if (model.StartDate == null || model.EndDate == null)
                    {
                        logs = await userService.SelectMany_vw_AspNetUserAccountLogsAsync(x => x.AspNetUserKey == model.Id);
                    }
                    else
                    {
                        logs = await userService.SelectMany_vw_AspNetUserAccountLogsAsync(x => x.AspNetUserKey == model.Id && x.CreatedDate >= startDate.Value && x.CreatedDate <= endDate.Value);
                    }
                    List<int?> logIds = logs.Select(x => x.AspNetUserLogsKey).ToList();
                    var logsData = userService.SelectMany_AspNetUserLogs(x => logIds.Contains(x.AspNetUserLogsKey)).OrderByDescending(x => x.CreatedDate);
                    foreach (var item in logsData)
                    {
                        items.Add(new LoginAnalysis { RequestType = item.RequestType, CreatedDate = item.CreatedDate.Date, Time = item.CreatedDate.TimeOfDay, IsStatus = item.IsStatus });
                    }
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }


            return Ok(items);

        }

        // GET: api/vw_LookupTypeGrid/{GridSetting}
        [HttpGet]
        [Route("api/loginAnalysis")]
        public async Task<IHttpActionResult> Getvw_AspNetUserLogsGrid([ModelBinder(typeof(APIGridModelBinder))] GridSetting gridSetting)
        {
            if (gridSetting.Where.rules.Count() > 0)
            {
                try
                {
                    DateTime? startDate = null;
                    DateTime? endDate = null;
                    int userId = int.MinValue;
                    List<Rule> rules = new List<Rule>();
                    if (gridSetting.SortColumn == "Time")
                    {
                        gridSetting.SortColumn = "CreatedDate";
                    }
                    foreach (var rule in gridSetting.Where.rules)
                    {
                        if (rule.field.ToLower() == "userid")
                        {
                            int.TryParse(rule.data, out userId);
                        }
                        if (rule.field.ToLower() == "startdate")
                        {
                            startDate = DateTime.ParseExact(rule.data, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                        if (rule.field.ToLower() == "enddate")
                        {
                            endDate = DateTime.ParseExact(rule.data, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                    }
                    rules = gridSetting.Where.rules.Where(x => x.field.ToLower() != "startdate" && x.field.ToLower() != "enddate" && x.field.ToLower() != "userid").ToList();
                    rules.Add(new Rule { field = "CreatedDate", data = startDate.ToString(), op = "ge" });
                    rules.Add(new Rule { field = "AspNetUserKey", data = userId.ToString(), op = "eq" });
                    rules.Add(new Rule { field = "CreatedDate", data = endDate.Value.AddDays(1).ToString(), op = "le" });
                    gridSetting.Where.rules = rules.ToArray();
                    var userLogs = await userService.Selectvw_AspNetUserAccountLogssByGridSettingAsync(gridSetting);
                    var pagedResult = new PagedResult<vw_AspNetUserAccountLogs>();
                    pagedResult.Results = userLogs;
                    pagedResult.RowCount = gridSetting.Count;
                    return Ok(pagedResult);
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }
            else
            {
                var pagedResult = new PagedResult<LoginAnalysis>();
                pagedResult.Results = new List<LoginAnalysis>();
                pagedResult.RowCount = gridSetting.Count;
                return Ok(pagedResult);
            }
        }
        #endregion
    }
}