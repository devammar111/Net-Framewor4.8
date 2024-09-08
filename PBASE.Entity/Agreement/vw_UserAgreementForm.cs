using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PBASE.Entity;
using PBASE.Entity.Helper;

namespace PBASE.Entity
{
    public partial class vw_UserAgreementForm : BaseEntity
    {
        [Key]
        public Guid UniqueId { get; set; }
        public int? AgreementId { get; set; }
        public int? UserId { get; set; }
        public string AgreementHeader { get; set; }
        public string AgreementText { get; set; }
        [NotMapped]
        public string EncryptedUniqueId { get { return CryptoEngine.Encrypt(UniqueId.ToString()); } }
    }
}

