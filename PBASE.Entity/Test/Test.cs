using System;
using System.ComponentModel.DataAnnotations;
namespace PBASE.Entity
{
    public partial class Test : TrackingEntity
    {
        [Key]
        public int? TestId { get; set; }
        public string TestText1 { get; set; }
        public string TestText2 { get; set; }
        public string TestText3 { get; set; }
        public string TestText4 { get; set; }
        public int? TestType1Id { get; set; }
        public int? TestType2Id { get; set; }
        public int? TestType3Id { get; set; }
        public DateTime? TestDate1 { get; set; }
        public DateTime? TestDate2 { get; set; }
        public DateTime? TestDate3 { get; set; }
        public bool? IsTest1 { get; set; }
        public bool? IsTest2 { get; set; }
        public bool? IsTest3 { get; set; }
        public int? TestAttachmentId { get; set; }
        public bool? IsArchived { get; set; }

    }

}

