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
    public class vw_AspNetUserAccountLogsGridController : BaseController
    {
        #region Initialization

        private readonly ILookupService lookupService;
        private readonly IUserService userService;

        public vw_AspNetUserAccountLogsGridController(ILookupService lookupService, IUserService userService
        )
        {
            this.lookupService = lookupService;
            this.userService = userService;
        }

        #endregion Initialization

        #region LoginAnalysis
        public async Task<IHttpActionResult> Getvw_AspNetUserAccountLogsGrid([ModelBinder(typeof(APIGridModelBinder))]GridSetting gridSetting)
        {
            try
            {
                var data = await userService.Selectvw_AspNetUserAccountLogssByGridSettingAsync(gridSetting);
                var pagedResult = new PagedResult<vw_AspNetUserAccountLogs>();
                pagedResult.Results = data.ToList();
                pagedResult.RowCount = gridSetting.Count;
                return Ok(pagedResult);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        #endregion
    }
}
