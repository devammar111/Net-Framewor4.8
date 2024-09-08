using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PBASE.Entity.Helper;
using System;

namespace PBASE.Entity
{
    public partial class vw_TestGrid : BaseEntity
    {
        [Key]
        public int? TestId { get; set; }
        public string TestText1 { get; set; }
        public string TestText2 { get; set; }
        public string TestText3 { get; set; }
        public DateTime? TestDate1 { get; set; }
        public DateTime? TestDate2 { get; set; }
        public DateTime? TestDate3 { get; set; }
        public bool? IsTest1 { get; set; }
        public bool? IsTest2 { get; set; }
        public bool? IsTest3 { get; set; }
        public string CreatedUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string TestType1 { get; set; }
        public string TestType2 { get; set; }
        public string TestType3 { get; set; }
        [NotMapped]
        public string EncryptedTestId { get { return CryptoEngine.Encrypt(TestId.ToString()); } }
    }

}
