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
    public partial class MessageController : BaseController
    {
        #region Initialization

        private readonly ILookupService lookupService;

        public MessageController(ILookupService lookupService
        )
        {
            this.lookupService = lookupService;
        }

        #endregion Initialization

        [HttpGet]
        [Route("Message/{id}")]
        public async Task<IHttpActionResult> GetMessage([FromUri]int id)
        {
            var model = new MessageViewModel();
            Message Message = await lookupService.SelectByMessageIdAsync(id);
            if (Message == null)
            {
                return NotFound();
            }

            ModelCopier.CopyModel(Message, model);
            return Ok(model);
        }

        [HttpPut]
        [Route("Message/{id}")]
        public async Task<IHttpActionResult> Message([FromUri]int id, MessageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Message = await lookupService.SelectByMessageIdAsync(id);
            if (Message != null)
            {
                ModelCopier.CopyModel(model, Message);
            }
            int effectedRows = await lookupService.SaveMessageFormAsync(Message);
            if (effectedRows <= 0)
            {
                return Conflict();
            }

            return Ok();
        }

        [HttpPost]
        [Route("Message")]
        public async Task<IHttpActionResult> Message(MessageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Message = new Message();
            ModelCopier.CopyModel(model, Message);
            int effectedRows = await lookupService.SaveMessageFormAsync(Message);
            if (effectedRows <= 0)
            {
                return NotFound();
            }
            return Ok(Message);
        }


        [HttpDelete]
        [Route("Message/{id}")]
        public async Task<IHttpActionResult> DeleteMessage(int id)
        {
            Message Message = await lookupService.SelectByMessageIdAsync(id);
            if (Message == null)
            {
                return NotFound();
            }

            int effectedRows = await lookupService.DeleteMessageFormAsync(id);
            if (effectedRows <= 0)
            {
                return BadRequest(lookupService.LastErrorMessage);
            }
            return Ok();
        }
    }
}