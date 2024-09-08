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
    public partial class InternalGridSettingController : BaseController
    {
        #region Initialization

        private readonly ILookupService lookupService;

        public InternalGridSettingController(ILookupService lookupService
        )
        {
            this.lookupService = lookupService;
        }

        #endregion Initialization

        [HttpGet]
        [Route("InternalGridSetting/{id}")]
        public async Task<IHttpActionResult> GetInternalGridSetting([FromUri]int id)
        {
            var model = new InternalGridSettingViewModel();
            InternalGridSetting InternalGridSetting = await lookupService.SelectByInternalGridSettingIdAsync(id);
            if (InternalGridSetting == null)
            {
                return NotFound();
            }

            ModelCopier.CopyModel(InternalGridSetting, model);
            return Ok(model);
        }

        [HttpPut]
        [Route("InternalGridSetting/{id}")]
        public async Task<IHttpActionResult> InternalGridSetting([FromUri]int id, InternalGridSettingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var InternalGridSetting = await lookupService.SelectByInternalGridSettingIdAsync(id);
            if (InternalGridSetting != null)
            {
                ModelCopier.CopyModel(model, InternalGridSetting);
            }
            int effectedRows = await lookupService.SaveInternalGridSettingFormAsync(InternalGridSetting);
            if (effectedRows <= 0)
            {
                return Conflict();
            }

            return Ok();
        }

        [HttpPost]
        [Route("InternalGridSetting")]
        public async Task<IHttpActionResult> InternalGridSetting(InternalGridSettingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var InternalGridSetting = new InternalGridSetting();
            ModelCopier.CopyModel(model, InternalGridSetting);
            int effectedRows = await lookupService.SaveInternalGridSettingFormAsync(InternalGridSetting);
            if (effectedRows <= 0)
            {
                return NotFound();
            }
            return Ok(InternalGridSetting);
        }


        [HttpDelete]
        [Route("InternalGridSetting/{id}")]
        public async Task<IHttpActionResult> DeleteInternalGridSetting(int id)
        {
            InternalGridSetting InternalGridSetting = await lookupService.SelectByInternalGridSettingIdAsync(id);
            if (InternalGridSetting == null)
            {
                return NotFound();
            }

            int effectedRows = await lookupService.DeleteInternalGridSettingFormAsync(id);
            if (effectedRows <= 0)
            {
                return BadRequest(lookupService.LastErrorMessage);
            }
            return Ok();
        }
    }
}