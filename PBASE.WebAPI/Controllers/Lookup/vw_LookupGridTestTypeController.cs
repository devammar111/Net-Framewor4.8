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
    public partial class vw_LookupGridTestTypeController : BaseController 
    {
        #region Initialization
        
        private readonly ITestService lookupService;
        
        public vw_LookupGridTestTypeController(ITestService lookupService
        )
        {
            this.lookupService = lookupService;
        }
        
        #endregion Initialization
        
        [HttpGet]
        [Route("vw_LookupGridTestType")]
        public IList<vw_LookupGridTestType> vw_LookupGridTestTypes()
        {
            return lookupService.SelectAllvw_LookupGridTestTypes().OrderBy(x => x.TestTypeDisplay).ToList();
        }
        
    }
}

