using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
using static PBASE.WebAPI.Helpers.CustomValidationAttribute;

namespace PBASE.WebAPI.ViewModels
{
    public partial class TemplateViewModel : BaseViewModel
    {
        [Display(Name = "Template Id")]
        [Range(0, int.MaxValue)]
        public int? TemplateId { get; set; }
        [Display(Name = "Type")]
        [Range(0, int.MaxValue)]
        public int? TemplateTypeId { get; set; }
        [Range(0, int.MaxValue)]
        public int? AttachmentId { get; set; }
        public IEnumerable<LookupEntity> vw_LookupTemplateTypes { get; set; }
        public List<LookupEntity> Attachment { get; set; }

        [DisplayName("Description")]
        [checkValidString]
        [StringLength(100)]
        public string Description { get; set; }

        [StringLength(100)]
        [checkValidString]
        public string AttachmentFileName { get; set; }
        public decimal? AttachmentFileSize { get; set; }
        [StringLength(200)]
        [checkValidString]
        public string AttachmentFileType { get; set; }
        [DisplayName("Attachment File Handle")]
        [StringLength(100)]
        [checkValidString]
        public string AttachmentFileHandle { get; set; }

        public List<LookupEntity> TemplateTags { get; set; }
        public IEnumerable<int> TemplateTagIds { get; set; }
    }
}