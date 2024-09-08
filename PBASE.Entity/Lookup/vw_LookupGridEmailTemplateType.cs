using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class vw_LookupGridEmailTemplateType : BaseEntity
    {
        [Key]
        public string EmailTemplateType { get; set; }
        public string EmailTemplateTypeDisplay { get; set; }
    }
}

