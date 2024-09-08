using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class vw_LookupGridRole : BaseEntity
    {
        [Key]
        public string UserType { get; set; }
        public string UserTypeDisplay { get; set; }
    }
}

