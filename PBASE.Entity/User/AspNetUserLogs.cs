using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class AspNetUserLogs : TrackingEntity
    {
        [Key]
        public int? AspNetUserLogsKey { get; set; }
        public int? AspNetUserKey { get; set; }
        public bool? IsStatus { get; set; }
        public string RequestType { get; set; }
        public string IPAddress { get; set; }
        public string Location { get; set; }

    }
}

