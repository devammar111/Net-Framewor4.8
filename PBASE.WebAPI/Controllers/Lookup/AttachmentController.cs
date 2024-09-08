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
    public partial class AttachmentController : BaseController
    {
        #region Initialization

        private readonly ILookupService lookupService;

        public AttachmentController(ILookupService lookupService
        )
        {
            this.lookupService = lookupService;
        }

        #endregion Initialization

        [HttpGet]
        [Route("Attachment/{id}")]
        public async Task<IHttpActionResult> GetAttachment([FromUri]int id)
        {
            var model = new AttachmentFormViewModel();
            Attachment Attachment = await lookupService.SelectByAttachmentIdAsync(id);
            if (Attachment == null)
            {
                return NotFound();
            }

            ModelCopier.CopyModel(Attachment, model);
            return Ok(model);
        }

        [HttpPut]
        [Route("Attachment/{id}")]
        public async Task<IHttpActionResult> Attachment([FromUri]int id, AttachmentFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Attachment = await lookupService.SelectByAttachmentIdAsync(id);
            if (Attachment != null)
            {
                ModelCopier.CopyModel(model, Attachment);
            }
            int effectedRows = await lookupService.SaveAttachmentFormAsync(Attachment);
            if (effectedRows <= 0)
            {
                return Conflict();
            }

            return Ok();
        }

        [HttpPost]
        [Route("Attachment")]
        public async Task<IHttpActionResult> Attachment(AttachmentFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Attachment = new Attachment();
            ModelCopier.CopyModel(model, Attachment);
            int effectedRows = await lookupService.SaveAttachmentFormAsync(Attachment);
            if (effectedRows <= 0)
            {
                return NotFound();
            }
            return Ok(Attachment);
        }


        [HttpDelete]
        [Route("Attachment/{id}")]
        public async Task<IHttpActionResult> DeleteAttachment(int id)
        {
            Attachment Attachment = await lookupService.SelectByAttachmentIdAsync(id);
            if (Attachment == null)
            {
                return NotFound();
            }

            int effectedRows = await lookupService.DeleteAttachmentFormAsync(id);
            if (effectedRows <= 0)
            {
                return BadRequest(lookupService.LastErrorMessage);
            }
            return Ok();
        }
    }
}