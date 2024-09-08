using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class UserGroupDashboardOption : TrackingEntity
    {
        [Key]
        public int? UserGroupDashboardOptionId { get; set; }
        public int? UserGroupId { get; set; }
        public int? DashboardOptionId { get; set; }
        public bool? IsArchived { get; set; }
        public int? AccessTypeId { get; set; }
    }
}

