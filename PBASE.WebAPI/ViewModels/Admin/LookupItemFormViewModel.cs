using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;
using PBASE.WebAPI.ViewModels;
using PBASE.Entity;
using static PBASE.WebAPI.Helpers.CustomValidationAttribute;

namespace PBASE.WebAPI.ViewModels
{
    public partial class LookupItemFormViewModel : BaseViewModel
    {
        
        public int LookupId { get; set; }
        public int LookupTypeId { get; set; }
        [Display(Name = "Lookup Name")]
        [checkValidString]
        public string LookupName { get; set; }
        [Display(Name = "Lookup Short Name")]
        [checkValidString]
        public string LookupNameShort { get; set; }
        [Display(Name = "Lookup Group")]
        [checkValidString]
        public string LookupNameType { get; set; }
        [Display(Name = "Lookup Extra Text")]
        [checkValidString]
        public string LookupExtraText { get; set; }
        [Display(Name = "Lookup Extra Decimal")]
        public decimal? LookupExtraDecimal { get; set; }
        [Display(Name = "Lookup Extra Date")]
        public DateTime? LookupExtraDate { get; set; }
        [checkValidString]
        public string LookupView { get; set; }
        [checkValidString]
        public string LookupViewLabel { get; set; }
        [checkValidString]
        public string LookupViewIdField { get; set; }
        [checkValidString]
        public string LookupViewDisplayField { get; set; }
        public int? LookupExtraInt { get; set; }
        public IEnumerable<SelectListItem> LookupExtraInts { get; set; }
        public int? PreviousLookupExtraInt { get; set; }
        public bool? IsArchived { get; set; }
        public int? SortOrder { get; set; }
    }
}

