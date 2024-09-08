using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class vw_LookupDashboardOptionGroup : BaseEntity
    {
        [Key, Column(Order = 0)]
        public int? DashboardOptionId { get; set; }
        [Key, Column(Order = 1)]
        public string DashboardOption { get; set; }
        [Key, Column(Order = 2)]
        public int? AspNetRoleId { get; set; }
    }
}

