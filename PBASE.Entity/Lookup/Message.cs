using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class Message : TrackingEntity
    {
        [Key]
        public int? MessageId { get; set; }
        public int? UserId { get; set; }
        public string MessageHeader { get; set; }
        public string MessageText { get; set; }
        public DateTime? MessageDate { get; set; }
        
    }
}

