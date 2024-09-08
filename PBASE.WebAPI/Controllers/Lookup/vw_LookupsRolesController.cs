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
    [RoutePrefix("api/lookuprole")]
    public partial class vw_LookupsRolesController : BaseController 
    {
        #region Initialization
        
        private readonly ILookupService lookupService;
        
        public vw_LookupsRolesController(ILookupService lookupService
        )
        {
            this.lookupService = lookupService;
        }
        
        #endregion Initialization
        
        [HttpGet]
        [Route("vw_lookupsroles")]
        public IEnumerable<LookupEntity> vw_LookupsRoles()
        {
            return lookupService.SelectAllvw_LookupRoles().Select(x => new LookupEntity() { LookupId = x.Id, LookupValue = x.Name, disabled = false, GroupBy = "ACTIVE" }).OrderBy(x => x.disabled).ThenBy(x => x.LookupValue);
        }
        
    }
}

