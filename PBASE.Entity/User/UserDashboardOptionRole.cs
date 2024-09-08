using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class UserDashboardOptionRole : TrackingEntity
    {
        [Key]
        public int? UserDashboardOptionRoleId { get; set; }
        public int? DashboardOptionId { get; set; }
        public int? AspNetRoleId { get; set; }
        public bool? IsArchived { get; set; }

    }
}

