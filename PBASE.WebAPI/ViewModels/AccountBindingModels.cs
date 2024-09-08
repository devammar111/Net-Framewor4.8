using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using PBASE.WebAPI.Helpers;
using PBASE.Entity.Helper;
using PBASE.Entity;
using static PBASE.WebAPI.Helpers.CustomValidationAttribute;

namespace PBASE.WebAPI.ViewModels
{
    // Models used as parameters to AccountController actions.

    public class AddExternalLoginBindingModel : BaseViewModel
    {
        [Required]
        [Display(Name = "External access token")]
        [checkValidString]
        [StringLength(200)]
        public string ExternalAccessToken { get; set; }
    }

    public class ChangePasswordBindingModel : BaseViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        [checkValidString]
        [StringLength(100)]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [checkValidString]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [checkValidString]
        [StringLength(100)]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class RegisterBindingModel: BaseViewModel
    {
        [Range(0, int.MaxValue)]
        public int? TitleId { get; set; }
        [checkValidString]
        [StringLength(3)]
        public string Other { get; set; }
        [DisplayName("Forenames in full")]
        [checkValidString]
        [StringLength(100)]
        public string FirstName { get; set; }
        [DisplayName("Professional surname")]
        [checkValidString]
        [StringLength(50)]
        public string LastName { get; set; }
        [DisplayName("Your Law Society roll number/Bar Council membership number")]
        [Range(0, int.MaxValue)]
        [checkValidString]
        public string Reference { get; set; }
        [checkValidString]
        [StringLength(100)]
        public string ProfessionalEmail { get; set; }
        public bool? IsAgreeTermsCondition { get; set; }
    }

    public class ProfileBindingModel : BaseViewModel
    {
        [Required]
        [DontUpdate]
        [Display(Name = "User name")]
        [checkValidString]
        [StringLength(256)]
        public string Username { get; set; }

        [Required]
        [DontUpdate]
        [Display(Name = "Email")]
        [checkValidString]
        [StringLength(256)]
        public string Email { get; set; }

        [DontUpdate]
        [Display(Name = "First Name")]
        [checkValidString]
        [StringLength(100)]
        public string FirstName { get; set; }

        [DontUpdate]
        [Display(Name = "Last Name")]
        [checkValidString]
        [StringLength(100)]
        public string LastName { get; set; }

        //Profile Attachment
        [checkValidString]
        public string AttachmentFileName { get; set; }
        public decimal? AttachmentFileSize { get; set; }
        [checkValidString]
        public string AttachmentFileType { get; set; }
        [checkValidString]
        public string AttachmentFileHandle { get; set; }

    }

    public class UserBindingModel : BaseViewModel
    {
        [DontUpdate]
        [Range(0, int.MaxValue)]
        public int Id { get; set; }

        public string EncryptedId
        {
            get { return CryptoEngine.Encrypt(Id.ToString()); }
        }

        [Required]
        [DontUpdate]
        [checkValidString]
        [StringLength(256)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [checkValidString]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [checkValidString]
        [StringLength(100)]
        public string LastName { get; set; }

        [Display(Name = "Assigned Roles")]
        [DontUpdate]
        [checkValidString]
        [StringLength(200)]
        public string[] AssignedRoles { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DontUpdate]
        [checkValidString]
        [StringLength(256)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [checkValidString]
        [StringLength(200)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [checkValidString]
        [StringLength(200)]
        public string ConfirmPassword { get; set; }

        [Range(0, int.MaxValue)]
        [Display(Name = "User Type")]
        public int UserTypeId { get; set; }

        [Display(Name = "Lock User")]
        public bool LockoutEnabled { get; set; }
        public IEnumerable<LookupEntity> vw_lookupuseraccesstype { get; set; }
        public IEnumerable<LookupEntity> vw_lookupmenuoptiongroups { get; set; }
        public IEnumerable<LookupEntity> vw_lookupdashboardoptionsgroups { get; set; }
        public IEnumerable<LookupEntity> vw_lookupusergroups { get; set; }
        public IEnumerable<int?> DashboardOptionIds { get; set; }
        public IEnumerable<int?> MenuOptionIds { get; set; }
        public int? UserGroupId { get; set; }
        public int? UserAccessTypeId { get; set; }
        public IEnumerable<LookupEntity> vw_lookuprole { get; set; }

        //Profile Attachment
        [checkValidString]
        public string AttachmentFileName { get; set; }
        public decimal? AttachmentFileSize { get; set; }
        [checkValidString]
        public string AttachmentFileType { get; set; }
        [checkValidString]
        public string AttachmentFileHandle { get; set; }

    }

    public class RegisterExternalBindingModel : BaseViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [checkValidString]
        [StringLength(256)]
        public string Email { get; set; }
    }

    public class RemoveLoginBindingModel : BaseViewModel
    {
        [Required]
        [Display(Name = "Login provider")]
        [checkValidString]
        [StringLength(128)]
        public string LoginProvider { get; set; }

        [Required]
        [Display(Name = "Provider key")]
        [checkValidString]
        [StringLength(128)]
        public string ProviderKey { get; set; }
    }

    public class SetPasswordBindingModel : BaseViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [checkValidString]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [checkValidString]
        [StringLength(100)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class CreateRoleBindingModel : BaseViewModel
    {
        [Required]
        [StringLength(256, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [checkValidString]
        [Display(Name = "Role Name")]
        public string Name { get; set; }

    }

    public class UsersInRoleModel : BaseViewModel
    {
        [Required]
        [Display(Name = "Id")]
        [Range(0, int.MaxValue)]
        public int Id { get; set; }

        [Display(Name = "Enrolled Users")]
        [Range(0, int.MaxValue)]
        public List<int> EnrolledUsers { get; set; }

        [Display(Name = "Removed Users")]
        [Range(0, int.MaxValue)]
        public List<int> RemovedUsers { get; set; }
    }

    public class RolesToAssignModel : BaseViewModel
    {
        [Required]
        [Display(Name = "Id")]
        [Range(0, int.MaxValue)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Roles")]
        public string[] AssignedRoles { get; set; }
    }

    public class ForgotPasswordBindingModel : BaseViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [checkValidString]
        [StringLength(256)]
        public string Email { get; set; }
    }

    public class ResetPasswordBindingModel : BaseViewModel
    {
        [Required]
        [Display(Name = "Id")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Id { get; set; }

        [Required]
        [Display(Name = "Token")]
        [checkValidString]
        public string Token { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [checkValidString]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "FormType")]
        [checkValidString]
        public string FormType { get; set; }
    }

    public class CheckTokenValidationBindingModel : BaseViewModel
    {
        [Required]
        [Display(Name = "Id")]
        [Range(0, int.MaxValue)]
        public int Id { get; set; }

        [Required]
        [checkValidString]
        [Display(Name = "Token")]
        public string Token { get; set; }
    }
}
