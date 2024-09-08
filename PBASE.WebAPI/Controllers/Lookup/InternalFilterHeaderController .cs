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
    public partial class InternalFilterHeaderController : BaseController
    {
        #region Initialization

        private readonly ILookupService lookupService;

        public InternalFilterHeaderController(ILookupService lookupService
        )
        {
            this.lookupService = lookupService;
        }

        #endregion Initialization

        [HttpGet]
        [Route("InternalFilterHeader/{id}")]
        public async Task<IHttpActionResult> GetConfiguration([FromUri]int id)
        {
            var model = new AttachmentFormViewModel();
            InternalFilterHeader InternalFilterHeader = await lookupService.SelectByInternalFilterHeaderIdAsync(id);
            if (InternalFilterHeader == null)
            {
                return NotFound();
            }

            ModelCopier.CopyModel(InternalFilterHeader, model);
            return Ok(model);
        }

        [HttpPut]
        [Route("InternalFilterHeader/{id}")]
        public async Task<IHttpActionResult> InternalFilterHeader([FromUri]int id, InternalFilterHeaderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var InternalFilterHeader = await lookupService.SelectByInternalFilterHeaderIdAsync(id);
            if (InternalFilterHeader != null)
            {
                ModelCopier.CopyModel(model, InternalFilterHeader);
            }
            int effectedRows = await lookupService.SaveInternalFilterHeaderFormAsync(InternalFilterHeader);
            if (effectedRows <= 0)
            {
                return Conflict();
            }

            return Ok();
        }

        [HttpPost]
        [Route("InternalFilterHeader")]
        public async Task<IHttpActionResult> InternalFilterHeader(InternalFilterHeaderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var InternalFilterHeader = new InternalFilterHeader();
            ModelCopier.CopyModel(model, InternalFilterHeader);
            int effectedRows = await lookupService.SaveInternalFilterHeaderFormAsync(InternalFilterHeader);
            if (effectedRows <= 0)
            {
                return NotFound();
            }
            return Ok(InternalFilterHeader);
        }


        [HttpDelete]
        [Route("InternalFilterHeader/{id}")]
        public async Task<IHttpActionResult> DeleteInternalFilterHeader(int id)
        {
            InternalFilterHeader InternalFilterHeader = await lookupService.SelectByInternalFilterHeaderIdAsync(id);
            if (InternalFilterHeader == null)
            {
                return NotFound();
            }

            int effectedRows = await lookupService.DeleteInternalFilterHeaderFormAsync(id);
            if (effectedRows <= 0)
            {
                return BadRequest(lookupService.LastErrorMessage);
            }
            return Ok();
        }
    }
}
