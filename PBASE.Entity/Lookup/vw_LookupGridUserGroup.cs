using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class vw_LookupGridUserGroup : BaseEntity
    {
        [Key]
        public string UserGroupName { get; set; }
        public string UserGroupNameDisplay { get; set; }
    }
}

