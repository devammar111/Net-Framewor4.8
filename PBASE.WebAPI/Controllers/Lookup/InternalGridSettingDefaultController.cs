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
    public partial class InternalGridSettingDefaultController : BaseController
    {
        #region Initialization

        private readonly ILookupService lookupService;

        public InternalGridSettingDefaultController(ILookupService lookupService
        )
        {
            this.lookupService = lookupService;
        }

        #endregion Initialization

        [HttpGet]
        [Route("InternalGridSettingDefault/{id}")]
        public async Task<IHttpActionResult> GetInternalGridSettingDefault([FromUri]int id)
        {
            var model = new InternalGridSettingDefaultViewModel();
            InternalGridSettingDefault InternalGridSettingDefault = await lookupService.SelectByInternalGridSettingDefaultIdAsync(id);
            if (InternalGridSettingDefault == null)
            {
                return NotFound();
            }

            ModelCopier.CopyModel(InternalGridSettingDefault, model);
            return Ok(model);
        }

        [HttpPut]
        [Route("InternalGridSettingDefault/{id}")]
        public async Task<IHttpActionResult> InternalGridSettingDefault([FromUri]int id, InternalGridSettingDefaultViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var InternalGridSettingDefault = await lookupService.SelectByInternalGridSettingDefaultIdAsync(id);
            if (InternalGridSettingDefault != null)
            {
                ModelCopier.CopyModel(model, InternalGridSettingDefault);
            }
            int effectedRows = await lookupService.SaveInternalGridSettingDefaultFormAsync(InternalGridSettingDefault);
            if (effectedRows <= 0)
            {
                return Conflict();
            }

            return Ok();
        }

        [HttpPost]
        [Route("InternalGridSettingDefault")]
        public async Task<IHttpActionResult> InternalGridSettingDefault(InternalGridSettingDefaultViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var InternalGridSettingDefault = new InternalGridSettingDefault();
            ModelCopier.CopyModel(model, InternalGridSettingDefault);
            int effectedRows = await lookupService.SaveInternalGridSettingDefaultFormAsync(InternalGridSettingDefault);
            if (effectedRows <= 0)
            {
                return NotFound();
            }
            return Ok(InternalGridSettingDefault);
        }


        [HttpDelete]
        [Route("InternalGridSettingDefault/{id}")]
        public async Task<IHttpActionResult> DeleteInternalGridSettingDefault(int id)
        {
            InternalGridSettingDefault InternalGridSettingDefault = await lookupService.SelectByInternalGridSettingDefaultIdAsync(id);
            if (InternalGridSettingDefault == null)
            {
                return NotFound();
            }

            int effectedRows = await lookupService.DeleteInternalGridSettingDefaultFormAsync(id);
            if (effectedRows <= 0)
            {
                return BadRequest(lookupService.LastErrorMessage);
            }
            return Ok();
        }
    }
}