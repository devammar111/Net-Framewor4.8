using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class vw_LookupGridUsers : BaseEntity
    {
        [Key]
        public string UserFullName { get; set; }
        public string UserFullNameDisplay { get; set; }
    }
}

