using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
using static PBASE.WebAPI.Helpers.CustomValidationAttribute;

namespace PBASE.WebAPI.ViewModels
{
    public partial class EmailTemplateFormViewModel : BaseViewModel
    {
        public int? EmailTemplateId { get; set; }
        public int? EmailTemplateTypeId { get; set; }
        public int? EmailTypeId { get; set; }
        [StringLength(100)]
        [DisplayName("Template Name")]
        [checkValidString]
        public string EmailTemplateName { get; set; }
        [DisplayName("Email Subject")]
        [checkValidString]
        [StringLength(200)]
        public string EmailSubject { get; set; }
        [DisplayName("CC")]
        [EmailAddress(ErrorMessage = "The CC field is not a valid email address.")]
        [StringLength(300)]
        [checkValidString]
        public string CcAddress { get; set; }
        [DisplayName("BCC")]
        [EmailAddress(ErrorMessage ="The BCC field is not a valid email address.")]
        [StringLength(300)]
        [checkValidString]
        public string BccAddress { get; set; }

        [DisplayName("Email Body")]
        public string EmailBody { get; set; }

        public IEnumerable<LookupEntity> vw_LookupEmailTemplateType { get; set; }
        public IEnumerable<LookupEntity> vw_LookupEmailType { get; set; }
        public IEnumerable<LookupEntity> vw_LookupFromEmailAddress { get; set; }
        public List<LookupEntity> EmailTemplateTags { get; set; }
        public IEnumerable<int> EmailTemplateTagIds { get; set; }
        public int? FromEmailAddressId { get; set; }

    }
}