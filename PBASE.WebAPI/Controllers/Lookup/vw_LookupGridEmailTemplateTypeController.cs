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
    public partial class vw_LookupGridEmailTemplateTypeController : BaseController 
    {
        #region Initialization
        
        private readonly ILookupService lookupService;
        
        public vw_LookupGridEmailTemplateTypeController(ILookupService lookupService
        )
        {
            this.lookupService = lookupService;
        }
        
        #endregion Initialization
        
        [HttpGet]
        [Route("vw_LookupGridEmailTemplateType")]
        public IList<vw_LookupGridEmailTemplateType> vw_LookupGridEmailTemplateTypes()
        {
            return lookupService.SelectAllvw_LookupGridEmailTemplateTypes().OrderBy(x => x.EmailTemplateType).ToList();
        }
        
    }
}

