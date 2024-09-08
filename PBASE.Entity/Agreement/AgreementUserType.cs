using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class AgreementUserType : TrackingEntity
    {
        [Key]
        public int? AgreementUserTypeId { get; set; }
        public int? AgreementId { get; set; }
        public int? UserTypeId { get; set; }
        public bool? IsArchived { get; set; }


    }
}

