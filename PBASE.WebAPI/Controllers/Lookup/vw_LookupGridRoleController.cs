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
    public partial class vw_LookupGridRoleController : BaseController 
    {
        #region Initialization
        
        private readonly ILookupService lookupService;
        
        public vw_LookupGridRoleController(ILookupService lookupService
        )
        {
            this.lookupService = lookupService;
        }
        
        #endregion Initialization
        
        [HttpGet]
        [Route("vw_LookupGridRole")]
        public IList<vw_LookupGridRole> vw_LookupGridRoles()
        {
            return lookupService.SelectAllvw_LookupGridRoles().OrderBy(x => x.UserType).ToList();
        }
        
    }
}

