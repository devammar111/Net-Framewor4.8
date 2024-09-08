using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class Template : TrackingEntity
    {
        [Key]
        public int? TemplateId { get; set; }
        public int? TemplateTypeId { get; set; }
        public int? AttachmentId { get; set; }
        public string Description { get; set; }
        public bool? IsArchived { get; set; }
    }
}

