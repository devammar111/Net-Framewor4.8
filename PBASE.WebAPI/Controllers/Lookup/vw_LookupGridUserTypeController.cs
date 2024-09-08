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
    public partial class vw_LookupGridUserTypeController : BaseController
    {
        #region Initialization

        private readonly ILookupService lookupService;

        public vw_LookupGridUserTypeController(ILookupService lookupService
        )
        {
            this.lookupService = lookupService;
        }

        #endregion Initialization

        [HttpGet]
        [Route("vw_lookupgridusertype")]
        public IList<vw_LookupGridUserType> vw_LookupGridUserTypes()
        {
            return lookupService.SelectAllvw_LookupGridUserTypes().OrderBy(x => x.UserType).ToList();
        }

    }
}

