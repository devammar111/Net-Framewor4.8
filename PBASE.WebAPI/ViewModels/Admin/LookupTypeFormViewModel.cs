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
    public partial class LookupTypeFormViewModel : BaseViewModel
    {
        
        [Display(Name = "Lookup Type Id")]
        public int? LookupTypeId { get; set; }
        [Display(Name = "Lookup Type Text")]
        [checkValidString]
        [StringLength(100)]
        public string LookupTypeText { get; set; }
        [Display(Name = "Is Locked")]
        public bool? IsLocked { get; set; }
        [Display(Name = "Lookup View")]
        [checkValidString]
        [StringLength(100)]
        public string LookupView { get; set; }
        [Display(Name = "Lookup View Label")]
        [checkValidString]
        [StringLength(50)]
        public string LookupViewLabel { get; set; }
        [Display(Name = "Lookup View Id Field")]
        [checkValidString]
        [StringLength(50)]
        public string LookupViewIdField { get; set; }
        [Display(Name = "Lookup View Display Field")]
        [checkValidString]
        [StringLength(50)]
        public string LookupViewDisplayField { get; set; }
    }
}

