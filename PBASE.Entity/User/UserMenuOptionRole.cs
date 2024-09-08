using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class UserMenuOptionRole : TrackingEntity
    {
        [Key]
        public int? UserMenuOptionRoleId { get; set; }
        public int? MenuOptionId { get; set; }
        public int? AspNetRoleId { get; set; }  
        public bool? IsArchived { get; set; }


    }
}

