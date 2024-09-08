using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class vw_LookupGridTemplateAllowedType : BaseEntity
    {
        [Key]
        public string TemplateAllowedType { get; set; }
        public string TemplateAllowedTypeDisplay { get; set; }
    }
}

