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
    public partial class SetPasswordFormViewModel : BaseViewModel
    {
        
        [Display(Name = "User Id")]
        public int UserId { get; set; }
        [Display(Name = "New Password")]
        [checkValidString]
        public string NewPassword { get; set; }
        [Display(Name = "Confirm New Password")]
        [checkValidString]
        public string ConfirmPassword { get; set; }
    }
}

