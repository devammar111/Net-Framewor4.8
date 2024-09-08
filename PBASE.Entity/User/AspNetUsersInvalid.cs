using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class AspNetUsersInvalid : TrackingEntity
    {
        [Key]
        public int? Id { get; set; }
        public string Email { get; set; }
        public int? AccessFailedCount { get; set; }
        public DateTime? LastAccessDate { get; set; }
        public string IPAddress { get; set; }

    }
}

