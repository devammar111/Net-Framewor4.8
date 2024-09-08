using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
using static PBASE.WebAPI.Helpers.CustomValidationAttribute;

namespace PBASE.WebAPI.ViewModels
{
    public partial class EmailViewModel : BaseViewModel
    {
        [Display(Name = "Email Id")]
        [Range(0, int.MaxValue)]
        public int? EmailId { get; set; }

        [DisplayName("From")]
        [checkValidString]
        [StringLength(300)]
        public string FromAddress { get; set; }

        [DisplayName("Reply")]
        [StringLength(300)]
        [checkValidString]
        public string ReplyAddress { get; set; }

        [DisplayName("To")]
        [StringLength(300)]
        [checkValidString]
        public string ToAddress { get; set; }

        [DisplayName("CC")]
        [StringLength(300)]
        [checkValidString]
        public string CCAddress { get; set; }

        [DisplayName("BCC")]
        [StringLength(300)]
        [checkValidString]
        public string BCCAddress { get; set; }

        [DisplayName("Subject")]
        [StringLength(300)]
        [checkValidString]
        public string Subject { get; set; }

        [DisplayName("Body")]
        public string Body { get; set; }

        public bool? IsHTML { get; set; }

        [DisplayName("Status")]
        [StringLength(500)]
        [checkValidString]
        public string Status { get; set; }

        [DisplayName("Requested Date")]
        public DateTime? RequestedDate { get; set; }

        [DisplayName("Sent Date")]
        public DateTime? SentDate { get; set; }

        public int? EmailTypeId { get; set; }

        public string ProcessName { get; set; }

        public int? ProcessId { get; set; }
        [DisplayName("Created By")]
        [checkValidString]
        public string UserFullName { get; set; }
        [DisplayName("Email Type")]
        [checkValidString]
        public string EmailType { get; set; }
        public List<Attachment> EmailAttachments { get; set; }


    }
}