using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class vw_LookupGridUserAccessType : BaseEntity
    {
        [Key]
        public string UserAccessType { get; set; }
        public string UserAccessTypeDisplay { get; set; }
    }
}

