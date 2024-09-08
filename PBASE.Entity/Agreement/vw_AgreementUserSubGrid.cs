using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PBASE.Entity;
using PBASE.Entity.Helper;

namespace PBASE.Entity
{
    public partial class vw_AgreementUserSubGrid : BaseEntity
    {
        [Key]
        public Guid UniqueId { get; set; }
        public int? AgreementId { get; set; }
        public int? UserId { get; set; }
        public string UserFullName { get; set; }
        public int? UserAgreementId { get; set; }
        public bool? IsAcceptDecline { get; set; }
        public DateTime? AcceptDeclineDate { get; set; }
        [NotMapped]
        public string EncryptedAgreementId { get { return CryptoEngine.Encrypt(AgreementId.ToString()); } }
    }
}

