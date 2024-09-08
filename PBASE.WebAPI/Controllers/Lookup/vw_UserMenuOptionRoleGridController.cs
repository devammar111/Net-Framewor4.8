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
    public partial class vw_UserMenuOptionRoleGridController : BaseController 
    {
        #region Initialization
        
        private readonly IUserService userService;
        private readonly ILookupService lookupService;

        public vw_UserMenuOptionRoleGridController(IUserService userService
        ,ILookupService lookupService
        )
        {
            this.userService = userService;
            this.lookupService = lookupService;
        }

        #endregion Initialization

        [HttpGet]
        [Route("api/vw_usermenuoptionrolegrid")]
        public async Task<IHttpActionResult> Getvw_UserMenuOptionRoleGrid([ModelBinder(typeof(APIGridModelBinder))]GridSetting gridSetting)
        {

            var data = await userService.Selectvw_UserMenuOptionRoleGridsByGridSettingAsync(gridSetting);
            var pagedResult = new PagedResult<vw_UserMenuOptionRoleGrid>();
            pagedResult.Results = data;
            pagedResult.RowCount = gridSetting.Count;
            return Ok(pagedResult);
        }

    }
}

