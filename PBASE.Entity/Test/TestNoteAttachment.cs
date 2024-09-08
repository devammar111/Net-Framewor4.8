using System;
using System.ComponentModel.DataAnnotations;
namespace PBASE.Entity
{
    public partial class TestNoteAttachment : TrackingEntity
    {
        [Key]
        public int? TestNoteAttachmentId { get; set; }
        public int? TestNoteId { get; set; }
        public int? TestNoteAttachentId { get; set; }
        public bool? IsArchived { get; set; }

    }


}

