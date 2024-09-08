using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class SystemAlert : TrackingEntity
    {
        [Key]
        public int? SystemAlertId { get; set; }
        public int? AlertTypeId { get; set; }
        public string AlertText { get; set; }
        public string CloseText { get; set; }
        public string OpenText { get; set; }
        public DateTime? ClosedDateTime { get; set; }
        public DateTime? OpenDateTime { get; set; }
        public int? WarningTime { get; set; }
        public int? OpenMessageTime { get; set; }
        public bool? IsArchived { get; set; }

    }
}

