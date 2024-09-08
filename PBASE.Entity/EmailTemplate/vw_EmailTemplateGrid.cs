using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PBASE.Entity;
using PBASE.Entity.Helper;

namespace PBASE.Entity
{
    public partial class vw_EmailTemplateGrid : BaseEntity
    {
        [Key]
        public int? EmailTemplateId { get; set; }
        public string EmailTemplateType { get; set; }
        public string EmailTemplateName { get; set; }
        public string EmailSubject { get; set; }
        public string TemplateAllowedType { get; set; }
        public string LastUpdateBy { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public string EmailType { get; set; }
        public int? FromEmailAddressId { get; set; }
        public string FromEmailAddress { get; set; }
        [NotMapped]
        public string EncryptedEmailTemplateId { get { return CryptoEngine.Encrypt(EmailTemplateId.ToString()); } }
    }
}

