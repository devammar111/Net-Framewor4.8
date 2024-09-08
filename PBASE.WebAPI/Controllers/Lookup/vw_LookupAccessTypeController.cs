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
    public partial class vw_LookupAccessTypeController : BaseController 
    {
        #region Initialization
        
        private readonly ILookupService lookupService;
        
        public vw_LookupAccessTypeController(ILookupService lookupService
        )
        {
            this.lookupService = lookupService;
        }
        
        #endregion Initialization
        
        [HttpGet]
        [Route("api/vw_LookupAccessType")]
        public IEnumerable<LookupEntity> vw_LookupAccessTypes()
        {
            return lookupService.SelectAllvw_LookupAccessTypes().Select(x => new LookupEntity() { LookupId = x.AccessTypeId, LookupValue = x.AccessType, disabled= x.IsArchived, GroupBy = x.IsArchived.Value ? "ARCHIVED" : "ACTIVE" }).OrderBy(x => x.disabled).ThenBy(x => x.LookupValue);
        }
        
    }
}

