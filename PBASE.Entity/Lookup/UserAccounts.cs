using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class UserAccounts : TrackingEntity
    {
        [Key]
        public int? Key { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? UserTypeId { get; set; }
        public int? UserAccountsId { get; set; }
        public Guid ID { get; set; }
        public string Tenant { get; set; }
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
        public string Email { get; set; }
        public bool? IsAccountVerified { get; set; }
        public DateTime? LastFailedPasswordReset { get; set; }
        public int? FailedPasswordResetCount { get; set; }
        public string MobileCode { get; set; }
        public DateTime? MobileCodeSent { get; set; }
        public string MobilePhoneNumber { get; set; }
        public DateTime? MobilePhoneNumberChanged { get; set; }
        public int? AccountTwoFactorAuthMode { get; set; }
        public int? CurrentTwoFactorAuthStatus { get; set; }
        public string VerificationKey { get; set; }
        public int? VerificationPurpose { get; set; }
        public DateTime? VerificationKeySent { get; set; }
        public string VerificationStorage { get; set; }
        public string HashedPassword { get; set; }

    }
}

