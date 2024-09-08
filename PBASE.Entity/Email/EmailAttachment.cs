using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
using PBASE.Entity.Helper;

namespace PBASE.Entity
{
    public partial class EmailAttachment : BaseEntity
    {
        [Key]
        public int? EmailAttachmentId { get; set; }
        public int? EmailId { get; set; }
        public int? AttachmentId { get; set; }
        public bool? IsArchived { get; set; }
    }
}

