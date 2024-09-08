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
    [RoutePrefix("api/Lookups")]
    public partial class vw_LookupObjectTypeController : BaseController
    {
        #region Initialization

        private readonly ILookupService lookupService;

        public vw_LookupObjectTypeController(ILookupService lookupService
        )
        {
            this.lookupService = lookupService;
        }

        #endregion Initialization

        [HttpGet]
        [Route("vw_LookupObjectType")]
        public IList<vw_LookupObjectType> vw_LookupObjectTypes()
        {
            return lookupService.SelectAllvw_LookupObjectTypes().OrderBy(x=> x.ObjectType).ToList();
        }

    }
}

