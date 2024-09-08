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

namespace PBASE.WebAPI.Controllers
{
    [RoutePrefix("api/lookup")]
    public partial class vw_LookupUserGroupController : BaseController
    {
        #region Initialization

        private readonly ILookupService lookupService;

        public vw_LookupUserGroupController(ILookupService lookupService
        )
        {
            this.lookupService = lookupService;
        }

        #endregion Initialization

        [HttpGet]
        [Route("vw_LookupUserGroup")]
        public async Task<IHttpActionResult> Getvw_LookupUserGroup([ModelBinder(typeof(APIGridModelBinder))]GridSetting gridSetting)
        {
            var data = await lookupService.Selectvw_LookupUserGroupsByGridSettingAsync(gridSetting);
            var pagedResult = new PagedResult<vw_LookupUserGroup>();
            pagedResult.Results = data;
            pagedResult.RowCount = gridSetting.Count;
            return Ok(pagedResult);
        }

    }
}

