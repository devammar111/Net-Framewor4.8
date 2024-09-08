using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class InterviewAppointment : TrackingEntity
    {
        [Key]
        public int? InterviewAppointmentId { get; set; }
        public int? InterviewDateId { get; set; }
        public DateTime? InterviewTime { get; set; }
        public int? Duration { get; set; }
        public string TimeSlot { get; set; }
    }
}

