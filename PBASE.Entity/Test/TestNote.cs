using System;
using System.ComponentModel.DataAnnotations;
namespace PBASE.Entity
{
    public partial class TestNote : TrackingEntity
    {
        [Key]
        public int? TestNoteId { get; set; }
        public int? TestId { get; set; }
        public int? TestNoteTypeId { get; set; }
        public string TestNoteText { get; set; }
        public bool? IsArchived { get; set; }
    }


}

