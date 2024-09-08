using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PBASE.Entity;
namespace PBASE.Entity
{
   public partial class Agreement : TrackingEntity
   {
      [Key]
      public int? AgreementId { get; set; }
      public string AgreementHeader { get; set; }
      public DateTime? AgreementDate { get; set; }
      public string AgreementText { get; set; }
      public bool IsArchived { get; set; }
      public int? OriginalAgreementId { get; set; }
      public decimal? VersionNo { get; set; }

      //[NotMapped]
      //public bool IsNotSent { get; set; }

   }
}

