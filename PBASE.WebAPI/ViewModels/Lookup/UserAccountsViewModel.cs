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
    public partial class UserAccountsViewModel : BaseViewModel
    {

        public int? Key { get; set; }
        [checkValidString]
        public string FirstName { get; set; }
        [checkValidString]
        public string LastName { get; set; }
        public int? UserTypeId { get; set; }
        public int? UpdatedUserId { get; set; }
        public Guid ID { get; set; }
        [checkValidString]
        public string Tenant { get; set; }
        [checkValidString]
        public string Username { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? LastUpdated { get; set; }
        public bool? IsAccountClosed { get; set; }
        public DateTime? AccountClosed { get; set; }
        public bool? IsLoginAllowed { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? LastFailedLogin { get; set; }
        public int? FailedLoginCount { get; set; }
        public DateTime? PasswordChanged { get; set; }
        public bool? RequiresPasswordReset { get; set; }
        [checkValidString]
        public string Email { get; set; }
        public bool? IsAccountVerified { get; set; }
        public DateTime? LastFailedPasswordReset { get; set; }
        public int? FailedPasswordResetCount { get; set; }
        [checkValidString]
        public string MobileCode { get; set; }
        public DateTime? MobileCodeSent { get; set; }
        [checkValidString]
        public string MobilePhoneNumber { get; set; }
        public DateTime? MobilePhoneNumberChanged { get; set; }
        public int? AccountTwoFactorAuthMode { get; set; }
        public int? CurrentTwoFactorAuthStatus { get; set; }
        [checkValidString]
        public string VerificationKey { get; set; }
        public int? VerificationPurpose { get; set; }
        public DateTime? VerificationKeySent { get; set; }
        [checkValidString]
        public string VerificationStorage { get; set; }
        [checkValidString]
        public string HashedPassword { get; set; }
    }
}