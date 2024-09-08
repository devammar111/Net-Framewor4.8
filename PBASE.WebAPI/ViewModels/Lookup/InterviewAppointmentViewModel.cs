using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;
using PBASE.WebAPI.ViewModels;
using PBASE.Entity;
using static PBASE.WebAPI.Helpers.CustomValidationAttribute;

namespace PBASE.WebAPI.ViewModels
{
    public partial class InterviewAppointmentViewModel : BaseViewModel
    {


        public int? InterviewAppointmentId { get; set; }
        public int? InterviewDateId { get; set; }
        public DateTime? InterviewTime { get; set; }
        public int? Duration { get; set; }
        [checkValidString]
        public string TimeSlot { get; set; }
    }
}