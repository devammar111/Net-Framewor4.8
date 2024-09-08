using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PBASE.Entity;
using PBASE.Entity.Helper;

namespace PBASE.Entity
{
    public partial class vw_UserAgreementSubGrid : BaseEntity
    {
        [Key]
        public int? UserAgreementId { get; set; }
        public int? UserId { get; set; }
        public int? AgreementId { get; set; }
        public int? OriginalAgreementId { get; set; }
        public string AgreementHeader { get; set; }
        public decimal? VersionNo { get; set; }
        public DateTime? AgreementDate { get; set; }
        public bool? IsAcceptDecline { get; set; }
        public DateTime? AcceptDeclineDate { get; set; }
        [NotMapped]
        public string EncryptedAgreementId { get { return CryptoEngine.Encrypt(AgreementId.ToString()); } }
    }
}

