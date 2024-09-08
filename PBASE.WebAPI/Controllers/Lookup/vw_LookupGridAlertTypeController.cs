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
    [RoutePrefix("api/lookup1")]
    public partial class vw_LookupGridAlertTypeController : BaseController
    {
        #region Initialization

        private readonly ILookupService lookupService;

        public vw_LookupGridAlertTypeController(ILookupService lookupService
        )
        {
            this.lookupService = lookupService;
        }

        #endregion Initialization

        [HttpGet]
        [Route("vw_LookupGridAlertType")]
        public IList<vw_LookupGridAlertType> vw_LookupGridAlertTypes()
        {
            return lookupService.SelectAllvw_LookupGridAlertTypes().OrderBy(x => x.AlertType).ToList();
        }

    }
}

