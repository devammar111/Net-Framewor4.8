using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class vw_UserDashboardOption : BaseEntity
    {
        [Key]
        public int? UserId { get; set; }
        public int? DashboardOptionId { get; set; }
        public string DashboardOption { get; set; }

    }
}

