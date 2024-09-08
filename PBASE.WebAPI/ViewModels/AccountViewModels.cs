using System;
using System.Collections.Generic;
using static PBASE.WebAPI.Helpers.CustomValidationAttribute;

namespace PBASE.WebAPI.ViewModels
{
    // Models returned by AccountController actions.

    public class ExternalLoginViewModel
    {
        public string Name { get; set; }
        [checkValidString]
        public string Url { get; set; }
        [checkValidString]
        public string State { get; set; }
    }

    public class ManageInfoViewModel
    {
        [checkValidString]
        public string LocalLoginProvider { get; set; }
        [checkValidString]
        public string Email { get; set; }

        public IEnumerable<UserLoginInfoViewModel> Logins { get; set; }

        public IEnumerable<ExternalLoginViewModel> ExternalLoginProviders { get; set; }
    }

    public class UserInfoViewModel
    {
        [checkValidString]
        public string Email { get; set; }

        public bool HasRegistered { get; set; }
        [checkValidString]

        public string LoginProvider { get; set; }
    }

    public class UserLoginInfoViewModel
    {
        [checkValidString]
        public string LoginProvider { get; set; }
        [checkValidString]

        public string ProviderKey { get; set; }
    }
}
