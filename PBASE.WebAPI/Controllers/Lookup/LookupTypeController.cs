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
    [MenuPermission(Menus.Lookups)]
    [RoutePrefix("api/lookup")]
    public partial class LookupTypeController : BaseController
    {
        #region Initialization

        private readonly ILookupService lookupService;

        public LookupTypeController(ILookupService lookupService
        )
        {
            this.lookupService = lookupService;
        }

        #endregion Initialization

        [HttpGet]
        [Route("LookupType/{id}")]
        public async Task<IHttpActionResult> GetLookupType([FromUri]int id)
        {
            var model = new LookupTypeFormViewModel();
            LookupType LookupType = await lookupService.SelectByLookupTypeIdAsync(id);
            if (LookupType == null)
            {
                return NotFound();
            }

            ModelCopier.CopyModel(LookupType, model);
            return Ok(model);
        }

        [HttpPut]
        [ReadOnlyValidation(Menus.Lookups)]
        [Route("LookupType/{id}")]
        public async Task<IHttpActionResult> LookupType([FromUri]int id, LookupTypeFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var LookupType = await lookupService.SelectByLookupTypeIdAsync(id);
            if (LookupType != null)
            {
                ModelCopier.CopyModel(model, LookupType);
            }
            int effectedRows = await lookupService.SaveLookupTypeFormAsync(LookupType);
            if (effectedRows <= 0)
            {
                return Conflict();
            }

            return Ok();
        }

        [HttpPost]
        [ReadOnlyValidation(Menus.Lookups)]
        [Route("LookupType")]
        public async Task<IHttpActionResult> LookupType(LookupTypeFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var LookupType = new LookupType();
            ModelCopier.CopyModel(model, LookupType);
            int effectedRows = await lookupService.SaveLookupTypeFormAsync(LookupType);
            if (effectedRows <= 0)
            {
                return NotFound();
            }
            return Ok(LookupType);
        }


        [HttpDelete]
        [ReadOnlyValidation(Menus.Lookups)]
        [Route("LookupType/{id}")]
        public async Task<IHttpActionResult> DeleteLookupType(int id)
        {
            LookupType LookupType = await lookupService.SelectByLookupTypeIdAsync(id);
            if (LookupType == null)
            {
                return NotFound();
            }

            int effectedRows = await lookupService.DeleteLookupTypeFormAsync(id);
            if (effectedRows <= 0)
            {
                return BadRequest(lookupService.LastErrorMessage);
            }
            return Ok();
        }
    }
}