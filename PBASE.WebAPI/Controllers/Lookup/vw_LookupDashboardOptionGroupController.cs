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
    public partial class vw_LookupDashboardOptionGroupController : BaseController
    {
        #region Initialization

        private readonly ILookupService lookupService;

        public vw_LookupDashboardOptionGroupController(ILookupService lookupService
        )
        {
            this.lookupService = lookupService;
        }

        #endregion Initialization

        [HttpGet]
        [Route("vw_LookupDashboardOptionGroup")]
        public IEnumerable<LookupEntity> vw_LookupDashboardOptionGroups()
        {
            return lookupService.SelectAllvw_LookupDashboardOptionGroups().Select(x => new LookupEntity() { LookupId = x.DashboardOptionId, LookupValue = x.DashboardOption, disabled = false, GroupBy = "ACTIVE" }).OrderBy(x => x.disabled).ThenBy(x => x.LookupValue);
        }

        [HttpGet]
        [Route("vw_LookupDashboardOptionGroup/{id}")]
        public IEnumerable<LookupEntity> vw_LookupDashboardOptionGroups(int? id)
        {
            return lookupService.SelectMany_vw_LookupDashboardOptionGroup(x => x.AspNetRoleId == id).Select(x => new LookupEntity() { LookupId = x.DashboardOptionId, LookupValue = x.DashboardOption, disabled = false, GroupBy = "ACTIVE" }).OrderBy(x => x.disabled).ThenBy(x => x.LookupValue);
        }


        //[HttpGet]
        //[Route("vw_LookupDashboardOptionGroup")]
        //public async Task<IHttpActionResult> Getvw_LookupDashboardOptionGroup([ModelBinder(typeof(APIGridModelBinder))]GridSetting gridSetting)
        //{
        //    var data = await lookupService.Selectvw_LookupDashboardOptionGroupsByGridSettingAsync(gridSetting);
        //    var pagedResult = new PagedResult<vw_LookupDashboardOptionGroup>();
        //    pagedResult.Results = data;
        //    pagedResult.RowCount = gridSetting.Count;
        //    return Ok(pagedResult);
        //}

    }
}

