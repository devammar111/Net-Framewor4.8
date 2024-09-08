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
    public partial class LookupFormViewModel : BaseViewModel
    {
        
        public int LookupTypeId { get; set; }

        [Display(Name = "Type Text")]
        [checkValidString]
        public string LookupTypeText { get; set; }

        [Display(Name = "Name")]
        [checkValidString]
        [StringLength(200)]
        public string LookupName { get; set; }

        [Display(Name = "Name Short ")]
        [checkValidString]
        [StringLength(200)]
        public string LookupNameShort { get; set; }

        [Display(Name = "Extra Int")]
        public int? LookupExtraInt { get; set; }

        [Display(Name = "Extra Text")]
        [checkValidString]
        [StringLength(2000)]
        public string LookupExtraText { get; set; }

        [Display(Name = "Extra Money ")]
        public decimal? LookupExtraMoney { get; set; }

        [Display(Name = "Extra Date ")]
        public DateTime? LookUpExtraDate { get; set; }

        [Display(Name = "Sort Order")]
        public int? SortOrder { get; set; }

        [Display(Name = "Is Archived")]
        public bool IsArchived { get; set; }

        public Dictionary<int, LookupItemFormViewModel> Options { get; set; }
    }
}

