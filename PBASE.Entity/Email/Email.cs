using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class Email : TrackingEntity
    {
        [Key]
        public int? EmailId { get; set; }
        public string FromAddress { get; set; }
        public string ReplyAddress { get; set; }
        public string ToAddress { get; set; }
        public string CCAddress { get; set; }
        public string BCCAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool? IsHTML { get; set; }
        public string Status { get; set; }
        public DateTime? RequestedDate { get; set; }
        public DateTime? SentDate { get; set; }
        public bool? IsArchived { get; set; }
        public int? EmailTypeId { get; set; }
        public string ProcessName { get; set; }
        public int? ProcessId { get; set; }
        public string MessageId { get; set; }
        [NotMapped]
        public bool IsNotSent { get; set; }

    }
}

