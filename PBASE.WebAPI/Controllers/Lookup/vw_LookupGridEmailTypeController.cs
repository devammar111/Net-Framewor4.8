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
    [RoutePrefix("api/lookups")]
    public partial class vw_LookupGridEmailTypeController : BaseController 
    {
        #region Initialization
        
        private readonly ILookupService lookupService;
        
        public vw_LookupGridEmailTypeController(ILookupService lookupService
        )
        {
            this.lookupService = lookupService;
        }
        
        #endregion Initialization
        
        [HttpGet]
        [Route("vw_LookupGridEmailType")]
        public IList<vw_LookupGridEmailType> vw_LookupGridEmailTypes()
        {
            return lookupService.SelectAllvw_LookupGridEmailTypes().OrderBy(x => x.EmailType).ToList();
        }
        
    }
}

