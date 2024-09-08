using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class vw_LookupUserAssignedRoles : BaseEntity
    {
        [Key]
        public int? UserId { get; set; }
        public string AssigendRoles { get; set; }

    }
}

