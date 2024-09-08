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
    public partial class vw_LookupGridTemplateAllowedTypeController : BaseController 
    {
        #region Initialization
        
        private readonly ILookupService lookupService;
        
        public vw_LookupGridTemplateAllowedTypeController(ILookupService lookupService
        )
        {
            this.lookupService = lookupService;
        }
        
        #endregion Initialization
        
        [HttpGet]
        [Route("vw_LookupGridTemplateAllowedType")]
        public IList<vw_LookupGridTemplateAllowedType> vw_LookupGridTemplateAllowedTypes()
        {
            return lookupService.SelectAllvw_LookupGridTemplateAllowedTypes().OrderBy(x => x.TemplateAllowedType).ToList();
        }
        
    }
}

