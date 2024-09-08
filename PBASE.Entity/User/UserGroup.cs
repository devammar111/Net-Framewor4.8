using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class UserGroup : TrackingEntity
    {
        [Key]
        public int? UserGroupId { get; set; }
        public string UserGroupName { get; set; }
        public int? AspNetRoleId { get; set; }
        public bool? IsArchived { get; set; }

    }
}

