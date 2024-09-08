using System;
using System.Net;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Web.Http;
using System.Web.Http.Description;
using System.Threading.Tasks;
using Probase.GridHelper;
using System.Web.Http.ModelBinding;
using System.Collections.Generic;
using PBASE.Entity;
using PBASE.Service;
using PBASE.WebAPI.Helpers;
using PBASE.WebAPI.ViewModels;
using PBASE.Repository.Infrastructure;
using System.Globalization;
using PBASE.Entity.Enum;

namespace PBASE.WebAPI.Controllers
{
    [MenuPermission(Menus.LoginAnalysis)]
    public partial class vw_AspNetUserLogsGridController : BaseController
    {
        #region Initialization

        private readonly IUserService userService;
        private readonly ILookupService lookupService;

        public vw_AspNetUserLogsGridController(IUserService userService
        , ILookupService lookupService
        )
        {
            this.userService = userService;
            this.lookupService = lookupService;
        }

        #endregion Initialization
        [HttpGet]
        [Route("api/vw_AspNetUserLogsGrid")]
        public async Task<IHttpActionResult> Getvw_AspNetUserLogsGrid([ModelBinder(typeof(APIGridModelBinder))]GridSetting gridSetting)
        {
            if (gridSetting.Where.rules.Count() > 0)
            {
                try
                {
                    DateTime? startDate = null;
                    DateTime? endDate = null;
                    foreach (var rule in gridSetting.Where.rules)
                    {
                        if (rule.field.ToLower() == "startdate")
                        {
                            startDate = DateTime.ParseExact(rule.data, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                        if (rule.field.ToLower() == "enddate")
                        {
                            endDate = DateTime.ParseExact(rule.data, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                    }
                    endDate = endDate.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                    gridSetting.Where.rules = gridSetting.Where.rules.Where(x => x.field.ToLower() != "startdate" && x.field.ToLower() != "enddate").ToArray();
                    var userLogs = await userService.Selectvw_AspNetUserLogsGridsByGridSettingAsync(gridSetting);
                    var userIds = userLogs.Select(x => x.Id).ToList().Distinct();
                    IEnumerable<vw_AspNetUserAccountLogs> logs = userService.SelectMany_vw_AspNetUserAccountLogs(x => userIds.Contains(x.AspNetUserKey) && x.CreatedDate >= startDate.Value && x.CreatedDate <= endDate.Value);
                    IEnumerable<vw_AspNetUserLogsGrid> data = from x in userLogs
                                                              select new vw_AspNetUserLogsGrid
                                                              {
                                                                  Id = x.Id,
                                                                  FullName = x.FullName,
                                                                  username = x.username,
                                                                  TotalLogin = logs.Where(l => l.AspNetUserKey == x.Id && l.RequestType == Enum.GetName(typeof(RequestType), 0)).Count(),
                                                                  LoginSuccess = logs.Where(l => l.AspNetUserKey == x.Id && l.RequestType == Enum.GetName(typeof(RequestType), 0) && l.IsStatus == true).Count(),
                                                                  LoginFailure = logs.Where(l => l.AspNetUserKey == x.Id && l.RequestType == Enum.GetName(typeof(RequestType), 0) && l.IsStatus == false).Count(),
                                                                  PasswordResetTotal = logs.Where(l => l.AspNetUserKey == x.Id && l.RequestType == Enum.GetName(typeof(RequestType), 1)).Count(),
                                                                  PasswordResetSuccess = logs.Where(l => l.AspNetUserKey == x.Id && l.RequestType == Enum.GetName(typeof(RequestType), 1) && l.IsStatus == true).Count(),
                                                                  PasswordResetFailure = logs.Where(l => l.AspNetUserKey == x.Id && l.RequestType == Enum.GetName(typeof(RequestType), 1) && l.IsStatus == false).Count(),
                                                                  ChangePasswordSuccess = logs.Where(l => l.AspNetUserKey == x.Id && l.RequestType == Enum.GetName(typeof(RequestType), 2) && l.IsStatus == true).Count(),
                                                                  ChangePasswordFailure = logs.Where(l => l.AspNetUserKey == x.Id && l.RequestType == Enum.GetName(typeof(RequestType), 2) && l.IsStatus == false).Count(),
                                                                  SuccessCreatedDate = x.SuccessCreatedDate,
                                                                  FailedCreatedDate = x.FailedCreatedDate,
                                                                  IPAddress = x.IPAddress,
                                                                  Location = x.Location

                                                              };
                    var pagedResult = new PagedResult<vw_AspNetUserLogsGrid>();
                    pagedResult.Results = data.ToList();
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
                var pagedResult = new PagedResult<vw_AspNetUserLogsGrid>();
                pagedResult.Results = new List<vw_AspNetUserLogsGrid>();
                pagedResult.RowCount = gridSetting.Count;
                return Ok(pagedResult);
            }
        }

        [HttpGet]
        [Route("api/GetLog/{id}")]
        public vw_AspNetUserLogsGrid GetLog(int? id)
        {
            return userService.SelectSingle_vw_AspNetUserLogsGrid(x => x.Id == id);
        }

    }
}

