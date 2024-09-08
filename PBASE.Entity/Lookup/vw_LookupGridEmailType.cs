using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class vw_LookupGridEmailType : BaseEntity
    {
        [Key]
        public string EmailType { get; set; }
        public string EmailTypeDisplay { get; set; }
    }
}

