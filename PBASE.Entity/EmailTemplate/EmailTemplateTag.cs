using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class EmailTemplateTag : TrackingEntity
    {
        [Key]
        public int? EmailTemplateTagId { get; set; }
        public int? EmailTemplateTypeId { get; set; }
        public string Tag { get; set; }
        public string TagDescription { get; set; }
        public bool? IsArchived { get; set; }
    }
}

