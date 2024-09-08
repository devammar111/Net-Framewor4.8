using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class InternalFilterHeader : TrackingEntity
    {
        [Key]
        public int? InternalFilterHeaderId { get; set; }
        public int? UserId { get; set; }
        public string FilterName { get; set; }
        public string Role { get; set; }
        public string GridName { get; set; }
        public string FilterValue { get; set; }
    }
}

