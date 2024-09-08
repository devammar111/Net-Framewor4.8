using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class vw_LookupList : BaseEntity
    {
        [Key]
        public int? LookupTypeId { get; set; }
        public string LookupList { get; set; }
    }
}

