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
    public partial class InterviewAppointmentController : BaseController
    {
        #region Initialization

        private readonly ILookupService lookupService;

        public InterviewAppointmentController(ILookupService lookupService
        )
        {
            this.lookupService = lookupService;
        }

        #endregion Initialization

        //[HttpGet]
        //[Route("InterviewAppointment/{id}")]
        //public async Task<IHttpActionResult> GetInterviewAppointment([FromUri]int id)
        //{
        //    var model = new InterviewAppointmentViewModel();
        //    InterviewAppointment InterviewAppointment = await lookupService.SelectByInterviewAppointmentIdAsync(id);
        //    if (InterviewAppointment == null)
        //    {
        //        return NotFound();
        //    }

        //    ModelCopier.CopyModel(InterviewAppointment, model);
        //    return Ok(model);
        //}

        //[HttpPut]
        //[Route("InterviewAppointment/{id}")]
        //public async Task<IHttpActionResult> InterviewAppointment([FromUri]int id, InterviewAppointmentViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var InterviewAppointment = await lookupService.SelectByInterviewAppointmentIdAsync(id);
        //    if (InterviewAppointment != null)
        //    {
        //        ModelCopier.CopyModel(model, InterviewAppointment);
        //    }
        //    int effectedRows = await lookupService.SaveInterviewAppointmentFormAsync(InterviewAppointment);
        //    if (effectedRows <= 0)
        //    {
        //        return Conflict();
        //    }

        //    return Ok();
        //}

        //[HttpPost]
        //[Route("InterviewAppointment")]
        //public async Task<IHttpActionResult> InterviewAppointment(InterviewAppointmentViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var InterviewAppointment = new InterviewAppointment();
        //    ModelCopier.CopyModel(model, InterviewAppointment);
        //    int effectedRows = await lookupService.SaveInterviewAppointmentFormAsync(InterviewAppointment);
        //    if (effectedRows <= 0)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(InterviewAppointment);
        //}


        //[HttpDelete]
        //[Route("InterviewAppointment/{id}")]
        //public async Task<IHttpActionResult> DeleteInterviewAppointment(int id)
        //{
        //    InterviewAppointment InterviewAppointment = await lookupService.SelectByInterviewAppointmentIdAsync(id);
        //    if (InterviewAppointment == null)
        //    {
        //        return NotFound();
        //    }

        //    int effectedRows = await lookupService.DeleteInterviewAppointmentFormAsync(id);
        //    if (effectedRows <= 0)
        //    {
        //        return BadRequest(lookupService.LastErrorMessage);
        //    }
        //    return Ok();
        //}
    }
}