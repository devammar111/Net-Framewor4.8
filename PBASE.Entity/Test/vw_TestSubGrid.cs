using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PBASE.Entity.Helper;
using System;

namespace PBASE.Entity
{
    public partial class vw_TestSubGrid : BaseEntity
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
        public string CreatedUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string EncryptedTestSubId { get { return CryptoEngine.Encrypt(TestSubId.ToString()); } }

    }

}
