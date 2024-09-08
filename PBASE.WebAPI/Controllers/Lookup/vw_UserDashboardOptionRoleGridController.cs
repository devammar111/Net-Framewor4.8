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
using PBASE.WebAPI.Controllers;

namespace PBASE.WebAPI.Controllers
{
    public partial class vw_UserDashboardOptionRoleGridController : BaseController
    {
        #region Initialization

        private readonly IUserService userService;
        private readonly ILookupService lookupService;

        public vw_UserDashboardOptionRoleGridController(IUserService userService
        , ILookupService lookupService
        )
        {
            this.userService = userService;
            this.lookupService = lookupService;
        }

        #endregion Initialization

        [HttpGet]
        [Route("api/vw_userdashboardoptionrolegrid")]
        public async Task<IHttpActionResult> Getvw_UserDashboardOptionRoleGrid([ModelBinder(typeof(APIGridModelBinder))]GridSetting gridSetting)
        {

            var data = await userService.Selectvw_UserDashboardOptionRoleGridsByGridSettingAsync(gridSetting);
            var pagedResult = new PagedResult<vw_UserDashboardOptionRoleGrid>();
            pagedResult.Results = data;
            pagedResult.RowCount = gridSetting.Count;
            return Ok(pagedResult);

        }
    }
}

