using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
using static PBASE.WebAPI.Helpers.CustomValidationAttribute;

namespace PBASE.WebAPI.ViewModels
{
   public partial class AgreementViewModel : BaseViewModel
   {
      public int? AgreementId { get; set; }
      [DisplayName("Agreement Name")]
      [StringLength(100)]
      [checkValidString]
      public string AgreementHeader { get; set; }
      public DateTime? AgreementDate { get; set; }
      [DisplayName("Agreement Text")]
      public string AgreementText { get; set; }
      public bool? IsArchived { get; set; }
      public int? OriginalAgreementId { get; set; }
      public decimal? VersionNo { get; set; }
      public IEnumerable<int> AgreementUserTypeIds { get; set; }
      public IEnumerable<LookupEntity> vw_lookupRole { get; set; }
   }
}