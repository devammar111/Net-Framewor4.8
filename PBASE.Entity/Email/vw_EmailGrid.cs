using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PBASE.Entity;
using PBASE.Entity.Helper;

namespace PBASE.Entity
{
    public partial class vw_EmailGrid : BaseEntity
    {
        [Key]
        public int? EmailId { get; set; }
        public string Subject { get; set; }
        public string ToAddress { get; set; }
        public string FromAddress { get; set; }
        public DateTime? RequestedDate { get; set; }
        public DateTime? SentDate { get; set; }
        public string Status { get; set; }
        public string EmailType { get; set; }
        public string UserFullName { get; set; }
        public int? AttachmentCount { get; set; }
        [NotMapped]
        public string EncryptedEmailId { get { return CryptoEngine.Encrypt(EmailId.ToString()); } }
    }
}

