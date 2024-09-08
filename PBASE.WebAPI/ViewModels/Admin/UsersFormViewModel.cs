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
    public partial class UsersFormViewModel : BaseViewModel
    {
        
        [Display(Name = "User Id")]
        public int UserId { get; set; }
        [Display(Name = "Username")]
        [checkValidString]
        public string Username { get; set; }
        [Display(Name = "Forename")]
        [checkValidString]
        public string FirstName { get; set; }
        [Display(Name = "Surname")]
        [checkValidString]
        public string LastName { get; set; }
        [Display(Name = "Password")]
        [checkValidString]
        public string UserPass { get; set; }
        [Display(Name = "Confirm Password")]
        [checkValidString]
        public string ConfirmUserPass { get; set; }
        [Display(Name = "Roles")]
        [checkValidString]
        public string Role { get; set; }
        [Display(Name = "Email Address")]
        [checkValidString]
        public string Email { get; set; }
        [Display(Name = "Is Archived")]
        public bool IsAccountClosed { get; set; }
        public IEnumerable<SelectListItem> RolesTypes { get; set; }
        [Display(Name = "User Type")]
        public int? UserTypeId { get; set; }
        public IEnumerable<SelectListItem> UserTypeIds { get; set; }
    }
}

