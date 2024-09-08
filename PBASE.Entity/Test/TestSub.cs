using System;
using System.ComponentModel.DataAnnotations;
namespace PBASE.Entity
{
    public partial class TestSub : TrackingEntity
    {
        [Key]
        public int? TestSubId { get; set; }
        public int? TestId { get; set; }
        public string TestSubText1 { get; set; }
        public string TestSubText2 { get; set; }
        public string TestSubText3 { get; set; }
        public string TestSubText4 { get; set; }
        public int? TestSubType1Id { get; set; }
        public int? TestSubType2Id { get; set; }
        public int? TestSubType3Id { get; set; }
        public DateTime? TestSubDate1 { get; set; }
        public DateTime? TestSubDate2 { get; set; }
        public DateTime? TestSubDate3 { get; set; }
        public bool? IsTestSub1 { get; set; }
        public bool? IsTestSub2 { get; set; }
        public bool? IsTestSub3 { get; set; }
        public int? TestSubAttachmentId { get; set; }
        public bool? IsArchived { get; set; }

    }


}
