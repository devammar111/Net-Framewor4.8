using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PBASE.Entity;
using PBASE.Entity.Helper;

namespace PBASE.Entity
{
    public partial class vw_AgreementVersionSubGrid : BaseEntity
    {
        [Key]
      public int? AgreementId { get; set; }
      public int? OriginalAgreementId { get; set; }
      public decimal? VersionNo { get; set; }
      public DateTime? AgreementDate { get; set; }
      public string CreatedBy { get; set; }
      public DateTime? CreatedDate { get; set; }
      public string UpdatedBy { get; set; }
      public DateTime? UpdatedDate { get; set; }
      [NotMapped]
        public string EncryptedAgreementId { get { return CryptoEngine.Encrypt(AgreementId.ToString()); } }
    }
}

