using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class TemplateTag : TrackingEntity
    {
        [Key]
        public int? TemplateTagId { get; set; }
        public int? TemplateTypeId { get; set; }
        public string Tag { get; set; }
        public string TagDescription { get; set; }
        public bool? IsArchived { get; set; }
    }
}

