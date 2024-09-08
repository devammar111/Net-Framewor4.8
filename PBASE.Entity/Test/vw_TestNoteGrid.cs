using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PBASE.Entity.Helper;
using System;

namespace PBASE.Entity
{
    public partial class vw_TestNoteGrid : BaseEntity
    {
        [Key]
        public int? TestNoteId { get; set; }
        public int? TestId { get; set; }
        public string TestNoteType { get; set; }
        public string TestNoteText { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string EncryptedTestNoteId { get { return CryptoEngine.Encrypt(TestNoteId.ToString()); } }

    }

}
