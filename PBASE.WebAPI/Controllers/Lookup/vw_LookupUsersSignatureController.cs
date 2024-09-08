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
    public partial class vw_LookupUsersSignatureController : BaseController 
    {
        #region Initialization
        
        private readonly ILookupService lookupService;
        
        public vw_LookupUsersSignatureController(ILookupService lookupService
        )
        {
            this.lookupService = lookupService;
        }

        #endregion Initialization

        [HttpGet]
        [Route("vw_LookupUsersSignature")]
        public IEnumerable<LookupEntity> vw_LookupUsersSignatures()
        {
            return lookupService.SelectAllvw_LookupUsersSignatures().Select(x => new LookupEntity() { LookupId = x.UserId, LookupValue = x.UserFullName, disabled = false, GroupBy = "ACTIVE" }).OrderBy(x => x.disabled).ThenBy(x => x.LookupValue);
        }

    }
}

