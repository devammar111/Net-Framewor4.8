using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PBASE.Entity.Helper;

namespace PBASE.Entity
{
    public partial class vw_TemplateGrid : BaseEntity
    {
        [Key]
        public int? TemplateId { get; set; }
        public int? AttachmentId { get; set; }
        public string TemplateType { get; set; }
        public string Description { get; set; }
        public string AttachmentFileHandle { get; set; }
        public int? LookupExtraInt { get; set; }
        public int? SortOrder { get; set; }
        public string TemplateAllowedType { get; set; }
        [NotMapped]
        public string EncryptedTemplateId { get { return CryptoEngine.Encrypt(TemplateId.ToString()); } }
        [NotMapped]
        public string EncryptedAttachmentId { get { return CryptoEngine.Encrypt(AttachmentId.ToString()); } }
    }
}
