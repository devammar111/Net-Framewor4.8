using System.Collections.Generic;
using System;
using PBASE.Entity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity.Helper;
using static PBASE.WebAPI.Helpers.CustomValidationAttribute;

namespace PBASE.WebAPI.ViewModels
{
    public partial class Lookup_LookupFormViewModel : BaseViewModel
    {
        public Lookup_LookupFormViewModel()
        {
        }
       
        public int LookupId { get; set; }
       
        public int LookupTypeId { get { return CryptoEngine.GetId(EncryptedLookupTypeId); } }

        [DisplayName("Lookup Name")]
        [checkValidString]
        [StringLength(200)]
        public string LookupName { get; set; }

        [DisplayName("Lookup Name Short")]
        [checkValidString]
        [StringLength(50)]
        public string LookupNameShort { get; set; }
        public int? LookupExtraInt { get; set; }
        public int? LookupExtraInt2 { get; set; }
        public int? LookupExtraInt3 { get; set; }
        public int? LookupExtraInt4 { get; set; }

        [DisplayName("Lookup Extra Text")]
        [checkValidString]
        [StringLength(2000)]
        public string LookupExtraText { get; set; }
        [DisplayName("Lookup Extra Decimal")]
        public decimal? LookupExtraDecimal { get; set; }
        public DateTime? LookupExtraDate { get; set; }

        [DisplayName("Sort Order")]
        public int? SortOrder { get; set; }
        
        public int? HistoricId { get; set; }
        public bool IsArchived { get; set; }
        [DisplayName("Lookup View")]
        [checkValidString]
        [StringLength(100)]
        public string LookupView { get; set; }
        [checkValidString]
        [StringLength(50)]
        public string LookupViewLabel { get; set; }
        [checkValidString]
        [StringLength(100)]
        [DisplayName("Lookup View 2")]
        public string LookupView2 { get; set; }
        [checkValidString]
        [StringLength(50)]
        public string LookupViewLabel2 { get; set; }
        [checkValidString]
        [StringLength(100)]
        [DisplayName("Lookup View 3")]
        public string LookupView3 { get; set; }
        [checkValidString]
        [StringLength(50)]
        public string LookupViewLabel3 { get; set; }
        [checkValidString]
        [StringLength(100)]
        [DisplayName("Lookup View 4")]
        public string LookupView4 { get; set; }
        [checkValidString]
        [StringLength(50)]
        public string LookupViewLabel4 { get; set; }
        public IList<AllLookupView> allLookupView { get; set; }
        public IList<AllLookupView> allLookupView2 { get; set; }
        public IList<AllLookupView> allLookupView3 { get; set; }
        public IList<AllLookupView> allLookupView4 { get; set; }
        public string EncryptedLookupTypeId { get; set; }
    }

}