using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PBASE.Entity;
using PBASE.Entity.Helper;

namespace PBASE.Entity
{
    public partial class vw_AgreementGrid : BaseEntity
    {
        [Key]
      public int? AgreementId { get; set; }
      public string AgreementHeader { get; set; }
      public DateTime? AgreementDate { get; set; }
      public string UserTypeList { get; set; }
      public int? AcceptedCount { get; set; }
      public int? DeclinedCount { get; set; }
      public int? OutstandingCount { get; set; }
      public decimal? VersionNo { get; set; }
      public int? OriginalAgreementId { get; set; }
      public bool? IsLocked { get; set; }
      public bool? IsLatest { get; set; }
      public bool? IsArchived { get; set; }
      [NotMapped]
        public string EncryptedAgreementId { get { return CryptoEngine.Encrypt(AgreementId.ToString()); } }
    }
}

