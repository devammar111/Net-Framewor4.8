using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class EmailTemplate : TrackingEntity
    {
        [Key]
        public int? EmailTemplateId { get; set; }
        public int? EmailTemplateTypeId { get; set; }
        public int? EmailTypeId { get; set; }
        public string EmailTemplateName { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public int? FromEmailAddressId { get; set; }
        public string CcAddress { get; set; }
        public string BccAddress { get; set; }
    }
}

