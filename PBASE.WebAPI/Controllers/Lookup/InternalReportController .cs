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
    public partial class InternalReportController : BaseController
    {
        #region Initialization

        private readonly ILookupService lookupService;

        public InternalReportController(ILookupService lookupService
        )
        {
            this.lookupService = lookupService;
        }

        #endregion Initialization

        [HttpGet]
        [Route("InternalReport/{id}")]
        public async Task<IHttpActionResult> GetInternalReport([FromUri]int id)
        {
            var model = new InternalReportViewModel();
            InternalReport InternalReport = await lookupService.SelectByInternalReportIdAsync(id);
            if (InternalReport == null)
            {
                return NotFound();
            }

            ModelCopier.CopyModel(InternalReport, model);
            return Ok(model);
        }

        [HttpPut]
        [Route("InternalReport/{id}")]
        public async Task<IHttpActionResult> InternalReport([FromUri]int id, InternalReportViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var InternalReport = await lookupService.SelectByInternalReportIdAsync(id);
            if (InternalReport != null)
            {
                ModelCopier.CopyModel(model, InternalReport);
            }
            int effectedRows = await lookupService.SaveInternalReportFormAsync(InternalReport);
            if (effectedRows <= 0)
            {
                return Conflict();
            }

            return Ok();
        }

        [HttpPost]
        [Route("InternalReport")]
        public async Task<IHttpActionResult> InternalReport(InternalReportViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var InternalReport = new InternalReport();
            ModelCopier.CopyModel(model, InternalReport);
            int effectedRows = await lookupService.SaveInternalReportFormAsync(InternalReport);
            if (effectedRows <= 0)
            {
                return NotFound();
            }
            return Ok(InternalReport);
        }


        [HttpDelete]
        [Route("InternalReport/{id}")]
        public async Task<IHttpActionResult> DeleteInternalReport(int id)
        {
            InternalReport InternalReport = await lookupService.SelectByInternalReportIdAsync(id);
            if (InternalReport == null)
            {
                return NotFound();
            }

            int effectedRows = await lookupService.DeleteInternalReportFormAsync(id);
            if (effectedRows <= 0)
            {
                return BadRequest(lookupService.LastErrorMessage);
            }
            return Ok();
        }
    }
}