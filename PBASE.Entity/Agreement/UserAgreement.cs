using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class UserAgreement : TrackingEntity
    {
        [Key]
        public int? UserAgreementId { get; set; }
        public int? AgreementId { get; set; }
        public int? UserId { get; set; }
        public bool? IsAcceptDecline { get; set; }
        public DateTime? AcceptDeclineDate { get; set; }

    }
}

