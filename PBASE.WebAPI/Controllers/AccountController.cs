using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using PBASE.WebAPI.ViewModels;
using PBASE.WebAPI.Providers;
using PBASE.WebAPI.Results;
using System.Linq;
using System.Configuration;
using Probase.GridHelper;
using PBASE.Service;
using PBASE.Entity;
using PBASE.Repository.Infrastructure;
using PBASE.WebAPI.Helpers;
using System.IO;
using System.Web.Hosting;
using PBASE.WebAPI;
using PBASE.Entity.Enum;
using System.Net;
using Newtonsoft.Json.Linq;
using PBASE.Entity.Helper;
using System.Text.RegularExpressions;

namespace PBASE.WebAPI.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : BaseController
    {
        #region Initialization
        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager = null;
        private ApplicationRoleManager _appRoleManager = null;
        private readonly IUserService UserService;
        private readonly IEmailService EmailService;
        private readonly ILookupService LookupService;
        private readonly ISystemAlertService SystemAlertService;
        private readonly IEmailTemplateService EmailTemplateService;
        private static readonly HttpClient client = new HttpClient();
        const int RESET_PASSWORD_CONFIRMATION = -9995; //Reset password confirmation > EmailTemplateId=38
        const int CREATE_PASSWORD_CONFIRMATION = -9996; //Create password confirmation > EmailTemplateId=37
        const int PASSWORD_RESET  = -9997; ////Reset password > EmailTemplateId=34
        const int CREATE_PASSWORD = -9998; //Create password > EmailTemplateId=33

        public AccountController(IUserService userService, ILookupService lookupService, IEmailService emailService, IEmailTemplateService emailTemplateService, ISystemAlertService systemAlertService)
        {
            UserService = userService;
            EmailService = emailService;
            LookupService = lookupService;
            EmailTemplateService = emailTemplateService;
            SystemAlertService = systemAlertService;
        }

        public AccountController(ApplicationUserManager userManager,
            ApplicationRoleManager appRoleManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AppRoleManager = appRoleManager;
            AccessTokenFormat = accessTokenFormat;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ApplicationRoleManager AppRoleManager
        {
            get
            {
                return _appRoleManager ?? Request.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
            private set
            {
                _appRoleManager = value;
            }
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        #endregion

        #region User Management

        // POST api/Account/Logout
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }

        //api/Account/SignOut
        [HttpGet]
        [AllowAnonymous]
        [Route("Signout")]
        public IHttpActionResult Signout()
        {
            var userId = GetUserId();
            if (userId != 0)
            {
                UserHelper.RemoveAllUserCache(userId);
            }
            return Ok("");
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser()
            {
                UserName = model.ProfessionalEmail,
                Email = model.ProfessionalEmail,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
            string password = model.FirstName + DateTime.Now.ToString();
            IdentityResult result = await UserManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
            string EncUserId = CryptoEngine.Encrypt(user.Id.ToString());
            var callbackUrl = ConfigurationManager.AppSettings["PBASEUrl"].ToString() + "authentication/change-password?id=" + EncUserId + "&verificationKey=" + HttpUtility.UrlEncode(code);
            IdentityMessage message = new IdentityMessage();
            message.Subject = "Registration Email";
            //message.Body = GenerateEmailBody(33, callbackUrl, app_info.ApplicationName, ConfigurationManager.AppSettings["PBASEUrl"].ToString(), user.FirstName + " " + user.LastName, app_info.CompanyName);
            message.Body = GenerateRegistrationEmailBody(callbackUrl, user.FirstName, user.Id);
            message.Destination = user.Email;
            SaveEmailToDatabase("Not set",message);

            return Ok();
        }

        // GET api/Account/CurrentUser
        [Route("CurrentUser")]
        public async Task<IHttpActionResult> GetCurrentUser()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await UserHelper.GetCurrentUser(GetUserId());

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // GET api/Account/User/1
        [Route("User")]
        public async Task<IHttpActionResult> GetUser()
        {
            UserBindingModel model = new UserBindingModel();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await UserManager.FindByIdAsync(GetUserId());

            if (user == null)
            {
                return NotFound();
            }

            model.vw_lookuprole = await LookupService.SelectAllvw_LookupRolesAsync();
            model.vw_lookupusergroups = await LookupService.SelectAllvw_LookupUserGroupsAsync();

            user.CopyTo(model);
            var role = await UserManager.GetRolesAsync(user.Id);
            model.AssignedRoles = role.ToArray();
            if (user.ProfileImageAttachmentId != null)
            {
                Attachment Attachment = await LookupService.SelectByAttachmentIdAsync(user.ProfileImageAttachmentId.Value);
                CopyEntityToViewModel(Attachment, model);
            }
         

            return Ok(model);
        }

        // POST api/Account/User
        [Route("User")]
        [RequestValidation]
        [ReadOnlyValidation(Menus.Users)]
        public async Task<IHttpActionResult> CreateUser(UserBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationUser user = new ApplicationUser();
            user.CopyFrom(model);

            IdentityResult result = await UserManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            IdentityResult roleResult = await AssignRolesToUser(new RolesToAssignModel { Id = user.Id, AssignedRoles = model.AssignedRoles });

            return Ok(user);
        }
       
        // DELTET api/Account/User/1 
        [HttpDelete]
        [ReadOnlyValidation(Menus.Users)]
        [Route("User/{id}")]
        public async Task<IHttpActionResult> DeleteUser(int id)
        {
            var user = await UserManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            IdentityResult result = await UserManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // PUT api/Account/Profile/1
        [HttpPut]
        [RequestValidation]
        [ReadOnlyValidation(Menus.Users)]
        [Route("Profile/{id}")]
        public async Task<IHttpActionResult> Profile([FromUri]int id, ProfileBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await UserManager.FindByIdAsync(id);
            user.ProfileImageAttachmentId = base.ProcessAttchment(LookupService, new Attachment
            {
                AttachmentId = user.ProfileImageAttachmentId.HasValue ? user.ProfileImageAttachmentId.Value : 0,
                AttachmentFileName = model.AttachmentFileName,
                AttachmentFileSize = model.AttachmentFileSize,
                AttachmentFileType = model.AttachmentFileType,
                AttachmentFileHandle = model.AttachmentFileHandle,
                ConnectedTable = "AspNetUsers",
                ConnectedField = "ProfileImageAttachmentId",
                IsArchived = false
            });

            user.CopyFrom(model);

            IdentityResult result = await UserManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            UserHelper.RemoveAllUserCache(id);

            return Ok();
        }

        private string GenerateRegistrationEmailBody(string callbackUrl, string name, int id)
        {
            string body = string.Empty;
            HttpBrowserCapabilities bc = HttpContext.Current.Request.Browser;
            string UserAgent = HttpContext.Current.Request.UserAgent;
            //var application = ApplicationsService.SelectSingle_Application(x => x.ApplicationUserId == id);
            //if (application != null)
            //{
            //    var vw_ApplicationEmail = ApplicationsService.SelectSingle_vw_ApplicationEmail(x => x.ApplicationId == application.ApplicationId);
            //    if (vw_ApplicationEmail != null)
            //    {
            //        using (StreamReader reader = new StreamReader(HostingEnvironment.MapPath("~/EmailTemplates/RegistrationEmail.html")))
            //        {
            //            body = reader.ReadToEnd();
            //        }
            //        body = body.Replace("{{site_url}}", ConfigurationManager.AppSettings["PBASEUrl"].ToString());
            //        body = body.Replace("{{logo_url}}", ConfigurationManager.AppSettings["PBASEUrl"].ToString() + "assets/images/logo-header.png");
            //        body = body.Replace("{{name}}", vw_ApplicationEmail.TitleLastName);
            //        body = body.Replace("{{action_url}}", callbackUrl);
            //        body = body.Replace("{{current_year}}", vw_ApplicationEmail.ApplicationYear);
            //    }
            //}
            return body;
        }

        #endregion

        #region Extra Methods

        // GET api/Account/UserInfo
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("UserInfo")]
        public UserInfoViewModel GetUserInfo()
        {
            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            return new UserInfoViewModel
            {
                Email = User.Identity.GetUserName(),
                HasRegistered = externalLogin == null,
                LoginProvider = externalLogin != null ? externalLogin.LoginProvider : null
            };
        }
        // GET api/Account/ManageInfo?returnUrl=%2F&generateState=true
        [Route("ManageInfo")]
        public async Task<ManageInfoViewModel> GetManageInfo(string returnUrl, bool generateState = false)
        {
            var user = await UserManager.FindByIdAsync(GetUserId());

            if (user == null)
            {
                return null;
            }

            List<UserLoginInfoViewModel> logins = new List<UserLoginInfoViewModel>();

            foreach (var linkedAccount in user.Logins)
            {
                logins.Add(new UserLoginInfoViewModel
                {
                    LoginProvider = linkedAccount.LoginProvider,
                    ProviderKey = linkedAccount.ProviderKey
                });
            }

            if (user.PasswordHash != null)
            {
                logins.Add(new UserLoginInfoViewModel
                {
                    LoginProvider = LocalLoginProvider,
                    ProviderKey = user.UserName,
                });
            }

            return new ManageInfoViewModel
            {
                LocalLoginProvider = LocalLoginProvider,
                Email = user.UserName,
                Logins = logins,
                ExternalLoginProviders = GetExternalLogins(returnUrl, generateState)
            };
        }
        // POST api/Account/AddExternalLogin
        [Route("AddExternalLogin")]
        [RequestValidation]
        [ReadOnlyValidation(Menus.Users)]
        public async Task<IHttpActionResult> AddExternalLogin(AddExternalLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

            AuthenticationTicket ticket = AccessTokenFormat.Unprotect(model.ExternalAccessToken);

            if (ticket == null || ticket.Identity == null || (ticket.Properties != null
                && ticket.Properties.ExpiresUtc.HasValue
                && ticket.Properties.ExpiresUtc.Value < DateTimeOffset.UtcNow))
            {
                return BadRequest("External login failure.");
            }

            ExternalLoginData externalData = ExternalLoginData.FromIdentity(ticket.Identity);

            if (externalData == null)
            {
                return BadRequest("The external login is already associated with an account.");
            }

            IdentityResult result = await UserManager.AddLoginAsync(GetUserId(),
                new UserLoginInfo(externalData.LoginProvider, externalData.ProviderKey));

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // GET api/Account/ExternalLogin
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [AllowAnonymous]
        [Route("ExternalLogin", Name = "ExternalLogin")]
        public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
        {
            if (error != null)
            {
                return Redirect(Url.Content("~/") + "#error=" + Uri.EscapeDataString(error));
            }

            if (!User.Identity.IsAuthenticated)
            {
                return new ChallengeResult(provider, this);
            }

            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            if (externalLogin == null)
            {
                return InternalServerError();
            }

            if (externalLogin.LoginProvider != provider)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                return new ChallengeResult(provider, this);
            }

            ApplicationUser user = await UserManager.FindAsync(new UserLoginInfo(externalLogin.LoginProvider,
                externalLogin.ProviderKey));

            bool hasRegistered = user != null;

            if (hasRegistered)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

                ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(UserManager,
                   OAuthDefaults.AuthenticationType);
                ClaimsIdentity cookieIdentity = await user.GenerateUserIdentityAsync(UserManager,
                    CookieAuthenticationDefaults.AuthenticationType);
                var closeAlerts = await SystemAlertService.SelectSingle_vw_SystemAlertIsClosedAsync(x => !string.IsNullOrWhiteSpace(x.CloseText));
                AuthenticationProperties properties = ApplicationOAuthProvider.CreateProperties(user.UserName, user.Id, closeAlerts);
                Authentication.SignIn(properties, oAuthIdentity, cookieIdentity);
            }
            else
            {
                IEnumerable<Claim> claims = externalLogin.GetClaims();
                ClaimsIdentity identity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
                Authentication.SignIn(identity);
            }

            return Ok();
        }

        // GET api/Account/ExternalLogins?returnUrl=%2F&generateState=true
        [AllowAnonymous]
        [Route("ExternalLogins")]
        public IEnumerable<ExternalLoginViewModel> GetExternalLogins(string returnUrl, bool generateState = false)
        {
            IEnumerable<AuthenticationDescription> descriptions = Authentication.GetExternalAuthenticationTypes();
            List<ExternalLoginViewModel> logins = new List<ExternalLoginViewModel>();

            string state;

            if (generateState)
            {
                const int strengthInBits = 256;
                state = RandomOAuthStateGenerator.Generate(strengthInBits);
            }
            else
            {
                state = null;
            }

            foreach (AuthenticationDescription description in descriptions)
            {
                ExternalLoginViewModel login = new ExternalLoginViewModel
                {
                    Name = description.Caption,
                    Url = Url.Route("ExternalLogin", new
                    {
                        provider = description.AuthenticationType,
                        response_type = "token",
                        client_id = Startup.PublicClientId,
                        redirect_uri = new Uri(Request.RequestUri, returnUrl).AbsoluteUri,
                        state = state
                    }),
                    State = state
                };
                logins.Add(login);
            }

            return logins;
        }

        // POST api/Account/RegisterExternal
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("RegisterExternal")]
        [RequestValidation]
        [ReadOnlyValidation(Menus.Users)]
        public async Task<IHttpActionResult> RegisterExternal(RegisterExternalBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var info = await Authentication.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return InternalServerError();
            }

            var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

            IdentityResult result = await UserManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            result = await UserManager.AddLoginAsync(user.Id, info.Login);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }
            return Ok();
        }

        #endregion

        #region Role Management

        // GET api/Account/vw_UserGrid
        [Route("vw_RoleGrid")]
        public async Task<IHttpActionResult> Getvw_RoleGrid([ModelBinder(typeof(APIGridModelBinder))]GridSetting gridSetting)
        {
            var data = await UserService.Selectvw_RoleGridsByGridSettingAsync(gridSetting);
            var pagedResult = new PagedResult<vw_RoleGrid>();
            pagedResult.Results = data;
            pagedResult.RowCount = gridSetting.Count;
            return Ok(pagedResult);
        }

        // GET api/Account/UserRoles/1
        [Route("UserRoles/{id}")]
        public async Task<IHttpActionResult> GetUserRoles([FromUri]int id)
        {
            var role = await UserManager.GetRolesAsync(id);

            if (role != null)
            {
                return Ok(role.ToArray());
            }

            return NotFound();
        }

        // GET api/Account/Role/1
        [Route("Role/{id}")]
        public async Task<IHttpActionResult> GetRole([FromUri]int id)
        {
            var role = await AppRoleManager.FindByIdAsync(id);

            if (role != null)
            {
                return Ok(role);
            }

            return NotFound();
        }

        // GET api/Account/Role
        [Route("Role")]
        public IHttpActionResult GetRoles()
        {
            var roles = AppRoleManager.Roles;

            return Ok(roles.ToArray().OrderBy(x => x.Name));
        }

        // POST api/Account/Role
        [Route("Role")]
        [RequestValidation]
        [ReadOnlyValidation(Menus.Users)]
        public async Task<IHttpActionResult> CreateRole(CreateRoleBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var role = new CustomRole { Name = model.Name };

            var result = await AppRoleManager.CreateAsync(role);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok(role);

        }

        // PUT api/Account/Role/1
        [HttpPut]
        [RequestValidation]
        [ReadOnlyValidation(Menus.Users)]
        [Route("Role/{id}")]
        public async Task<IHttpActionResult> UpdateRole([FromUri]int id, CreateRoleBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var role = await AppRoleManager.FindByIdAsync(id);
            role.Name = model.Name;

            var result = await AppRoleManager.UpdateAsync(role);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok(role);

        }

        // DELETE api/Account/Role/1
        [HttpDelete]
        [ReadOnlyValidation(Menus.Users)]
        [Route("Role/{id}")]
        public async Task<IHttpActionResult> DeleteRole([FromUri]int id)
        {

            var role = await AppRoleManager.FindByIdAsync(id);

            if (role != null)
            {
                IdentityResult result = await AppRoleManager.DeleteAsync(role);

                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }

                return Ok();
            }

            return NotFound();

        }

        // POST api/Account/ManageUsersInRole
        [Route("ManageUsersInRole")]
        [RequestValidation]
        [ReadOnlyValidation(Menus.Users)]
        public async Task<IHttpActionResult> ManageUsersInRole(UsersInRoleModel model)
        {
            var role = await AppRoleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                ModelState.AddModelError("", "Role does not exist");
                return BadRequest(ModelState);
            }

            foreach (int user in model.EnrolledUsers)
            {
                var appUser = await UserManager.FindByIdAsync(user);

                if (appUser == null)
                {
                    ModelState.AddModelError("", String.Format("User: {0} does not exists", user));
                    continue;
                }

                if (!UserManager.IsInRole(user, role.Name))
                {
                    IdentityResult result = await UserManager.AddToRoleAsync(user, role.Name);

                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", String.Format("User: {0} could not be added to role", user));
                    }

                }
            }

            foreach (int user in model.RemovedUsers)
            {
                var appUser = await UserManager.FindByIdAsync(user);

                if (appUser == null)
                {
                    ModelState.AddModelError("", String.Format("User: {0} does not exists", user));
                    continue;
                }

                IdentityResult result = await UserManager.RemoveFromRoleAsync(user, role.Name);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", String.Format("User: {0} could not be removed from role", user));
                }
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok();
        }

        // PUT api/Account/AssignRoles
        [HttpPut]
        [RequestValidation]
        [ReadOnlyValidation(Menus.Users)]
        [Route("AssignRoles")]
        public async Task<IHttpActionResult> AssignRoles(RolesToAssignModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var appUser = await UserManager.FindByIdAsync(model.Id);

            if (appUser == null)
            {
                return NotFound();
            }

            var currentRoles = await UserManager.GetRolesAsync(appUser.Id);

            var rolesNotExists = model.AssignedRoles.Except(AppRoleManager.Roles.Select(x => x.Name)).ToArray();

            if (rolesNotExists.Count() > 0)
            {

                ModelState.AddModelError("", string.Format("Roles '{0}' does not exits in the system", string.Join(",", rolesNotExists)));
                return BadRequest(ModelState);
            }

            IdentityResult removeResult = await UserManager.RemoveFromRolesAsync(appUser.Id, currentRoles.ToArray());

            if (!removeResult.Succeeded)
            {
                ModelState.AddModelError("", "Failed to remove user roles");
                return BadRequest(ModelState);
            }

            IdentityResult addResult = await UserManager.AddToRolesAsync(appUser.Id, model.AssignedRoles);

            if (!addResult.Succeeded)
            {
                ModelState.AddModelError("", "Failed to add user roles");
                return BadRequest(ModelState);
            }

            return Ok();
        }

        #endregion Role Management

        #region Password Management

        // POST api/Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [Route("ForgotPassword")]
        public async Task<IHttpActionResult> ForgotPassword(ForgotPasswordBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    return NotFound();
                }

                // Send an email with this link
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                string EncUserId = CryptoEngine.Encrypt(user.Id.ToString());
                var callbackUrl = ConfigurationManager.AppSettings["PBASEUrl"].ToString() + "authentication/change-password?id=" + EncUserId + "&verificationKey=" + HttpUtility.UrlEncode(code);

                var ApplicationInformation = EmailService.SelectAllApplicationInformations();
                ApplicationInformation app_info = ApplicationInformation.ToList()[0];

                IdentityMessage message = new IdentityMessage();
                var Data = GenerateEmailBody(PASSWORD_RESET, callbackUrl, app_info.ApplicationName, ConfigurationManager.AppSettings["PBASEUrl"].ToString(), user.FirstName + " " + user.LastName, app_info.CompanyName);
                message.Subject = Data[2];
                message.Body = Data[0];


                //message.Body = GenerateResetEmailBody(callbackUrl, "PasswordReset", ConfigurationManager.AppSettings["PBASEUrl"].ToString(), user.FirstName);
                message.Destination = user.Email;
                SaveEmailToDatabase(Data[1].ToString(), message);
                return Ok(true);
            }

            // If we got this far, something failed, redisplay form
            return BadRequest(ModelState);
        }

        // POST api/Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [Route("ResetPassword")]
        public virtual async Task<IHttpActionResult> ResetPassword(ResetPasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            String clientIP = GetIPAddress();
            string location = await GetLocationByIpAsync(clientIP);
            int UserId = Convert.ToInt32(CryptoEngine.Decrypt(model.Id.ToString()));
            IdentityResult result = await UserManager.ResetPasswordAsync(UserId, model.Token, model.Password);

            if (!result.Succeeded)
            {
                UserLogHelper.LogAccountActivity(UserId, Enum.GetName(typeof(RequestType), 1), false, clientIP, location);
                return GetErrorResult(result);
            }
            UserLogHelper.LogAccountActivity(UserId, Enum.GetName(typeof(RequestType), 1), true, clientIP, location);
            // Send an email with this link
            var user = await UserManager.FindByIdAsync(UserId);
            user.AccessFailedCount = 0;
            await UserManager.UpdateAsync(user);
            var callbackUrl = ConfigurationManager.AppSettings["PBASEUrl"].ToString() + "authentication/login";
            IdentityMessage message = new IdentityMessage();
            var ApplicationInformation = EmailService.SelectAllApplicationInformations();
            ApplicationInformation app_info = ApplicationInformation.ToList()[0];

            var Data = GenerateEmailBody(CREATE_PASSWORD_CONFIRMATION, callbackUrl, app_info.ApplicationName, ConfigurationManager.AppSettings["PBASEUrl"].ToString(), user.FirstName + " " + user.LastName, app_info.CompanyName);
            message.Subject = Data[2];
            message.Body = Data[0];
            message.Destination = user.Email;
            SaveEmailToDatabase(Data[1].ToString(), message);

            return Ok(true);
        }

        // PUT api/Account/ChangePassword/1
        [HttpPut]
        [RequestValidation]
        [ReadOnlyValidation(Menus.Users)]
        [Route("ChangePassword/{id}")]
        public async Task<IHttpActionResult> ChangePassword([FromUri]int id, ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            String clientIP = GetIPAddress();
            string location = await GetLocationByIpAsync(clientIP);

            if (model.OldPassword == model.NewPassword)
            {
                UserLogHelper.LogAccountActivity(id, Enum.GetName(typeof(RequestType), 2), false, clientIP, location);
                return BadRequest("New password should be different from old one.");
            }

            IdentityResult result = await UserManager.ChangePasswordAsync(id, model.OldPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                UserLogHelper.LogAccountActivity(id, Enum.GetName(typeof(RequestType), 2), false, clientIP, location);
                return BadRequest(result.Errors.FirstOrDefault());
            }
            UserLogHelper.LogAccountActivity(id, Enum.GetName(typeof(RequestType), 2), true, clientIP, location);

            return Ok();
        }

        // PUT api/Account/SetPassword/1
        [HttpPut]
        [Decrypt("id")]
        [Route("SetPassword/{id}")]
        public async Task<IHttpActionResult> SetPassword([FromUri]string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await UserManager.FindByIdAsync(Convert.ToInt32(id));
            String clientIP = GetIPAddress();
            var roles = await UserManager.GetRolesAsync(Convert.ToInt32(id));
            var siteUrl = ConfigurationManager.AppSettings["PBASEUrl"].ToString();
            string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
            string EncUserId = CryptoEngine.Encrypt(user.Id.ToString());
            var callbackUrl = siteUrl + "authentication/change-password?id=" + EncUserId + "&password=Set" + "&verificationKey=" + HttpUtility.UrlEncode(code);
            var ApplicationInformation = EmailService.SelectAllApplicationInformations();
            ApplicationInformation app_info = ApplicationInformation.ToList()[0];

            IdentityMessage message = new IdentityMessage();
            var Data = GenerateEmailBody(CREATE_PASSWORD, callbackUrl, app_info.ApplicationName, ConfigurationManager.AppSettings["PBASEUrl"].ToString(), user.FirstName + " " + user.LastName, app_info.CompanyName);
            message.Subject = Data[2];
            message.Body = Data[0];
            message.Destination = user.Email;
            SaveEmailToDatabase(Data[1].ToString(), message);

            return Ok(user);
        }

        #endregion

        protected string GetIPAddress()
        {
            HttpRequest currentRequest = HttpContext.Current.Request;
            string ipList = currentRequest.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipList))
            {
                return ipList.Split(',')[0];
            }

            return currentRequest.ServerVariables["REMOTE_ADDR"];
        }

        public int SaveEmailToDatabase(string From, IdentityMessage message)
        {
            Email email = new Email();

            DateTime currentDateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "UTC", "GMT Standard Time");
            email.FormMode = FormMode.Create;
            email.RequestedDate = currentDateTime;
            email.FromAddress = From;
            email.ReplyAddress = From;
            email.ToAddress = message.Destination;
            email.Subject = message.Subject;
            email.Body = message.Body;
            email.EmailTypeId = (int)EmailType.System;
            email.IsHTML = true;
            email.IsArchived = false;
            EmailService.SaveEmailForm(email);

            return email.EmailId.Value;
        }

        public static async Task<string> GetLocationByIpAsync(string IP)
        {
            try
            {
                var ipParts = IP.Split(':');
                if (ipParts[0] == "")
                {
                    return "";
                }
                string loction = "";
                var values = new Dictionary<string, string>
                {
                    { "ip", ipParts[0] }
                };

                var content = new FormUrlEncodedContent(values);

                var response = await client.PostAsync("https://iplocation.com", content);

                var responseString = await response.Content.ReadAsStringAsync();
                var obj = JObject.Parse(responseString);
                var City = (string)obj["city"];
                var Country = (string)obj["country_name"];
                if (City != null || City != "")
                {
                    loction = City;
                }
                if (Country != null || Country != "")
                {
                    if (loction != "")
                    {
                        loction = loction + ", " + Country;
                    }
                    else
                    {
                        loction = Country;
                    }
                }
                return loction;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception Occurs", ex); ;
            }
        }

        #region Helpers

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }

            public IList<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }
        }

        private static class RandomOAuthStateGenerator
        {
            private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                int strengthInBytes = strengthInBits / bitsPerByte;

                byte[] data = new byte[strengthInBytes];
                _random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }

        private async Task<IdentityResult> AssignRolesToUser(RolesToAssignModel model)
        {
            var appUser = await UserManager.FindByIdAsync(model.Id);

            var currentRoles = await UserManager.GetRolesAsync(appUser.Id);

            var rolesNotExists = model.AssignedRoles.Except(AppRoleManager.Roles.Select(x => x.Name)).ToArray();
            if (rolesNotExists.Count() > 0)
            {
                return new IdentityResult();
            }

            IdentityResult removeResult = await UserManager.RemoveFromRolesAsync(appUser.Id, currentRoles.ToArray());

            if (!removeResult.Succeeded)
            {
                return new IdentityResult();
            }

            IdentityResult addResult = await UserManager.AddToRolesAsync(appUser.Id, model.AssignedRoles);
            if (!addResult.Succeeded)
            {
                return new IdentityResult();
            }

            return addResult;
        }

        private string GenerateResetEmailBody(string callbackUrl, string template, string url, string name, string appName = "PBASE")
        {
            string body = string.Empty;

            HttpBrowserCapabilities bc = HttpContext.Current.Request.Browser;
            string UserAgent = HttpContext.Current.Request.UserAgent;

            using (StreamReader reader = new StreamReader(HostingEnvironment.MapPath("~/EmailTemplates/" + template + ".html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{{site_url}}", url);
            body = body.Replace("{{name}}", name);
            body = body.Replace("{{token_time}}", "24 hours");
            body = body.Replace("{{action_url}}", callbackUrl);
            body = body.Replace("{{operating_system}}", "[ " + UserAgent + " ]");
            body = body.Replace("{{browser_name}}", "[ " + bc.Browser + " " + bc.Version + " ]");
            body = body.Replace("{{current_year}}", DateTime.Now.Year.ToString());
            body = body.Replace("{{app_name}}", appName);
            return body;
        }

        private string GenerateConfirmationEmailBody(string callbackUrl, string name)
        {
            string body = string.Empty;
            HttpBrowserCapabilities bc = HttpContext.Current.Request.Browser;
            string UserAgent = HttpContext.Current.Request.UserAgent;

            using (StreamReader reader = new StreamReader(HostingEnvironment.MapPath("~/EmailTemplates/ConfirmationEmail.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{{site_url}}", ConfigurationManager.AppSettings["PBASEUrl"].ToString());
            body = body.Replace("{{logo_url}}", ConfigurationManager.AppSettings["PBASEUrl"].ToString() + "assets/images/logo-header.png");
            body = body.Replace("{{name}}", name);
            body = body.Replace("{{action_url}}", callbackUrl);
            body = body.Replace("{{current_year}}", DateTime.Now.Year.ToString());
            return body;
        }


        private List<string> GenerateEmailBody(int EmailTemplateTypeId, string callbackUrl, string appName, string url, string name, string companyName)
        {
            string body = string.Empty;
            try
            {
                var template = EmailTemplateService.SelectSingle_EmailTemplate(x => x.EmailTemplateTypeId == EmailTemplateTypeId);
                vw_LookupFromEmailAddress LookupFromEmailAddress = LookupService.SelectSingle_vw_LookupFromEmailAddress(x => x.FromEmailAddressId == template.FromEmailAddressId);
                body = template.EmailBody;
                var emailSubject = GetEmailSubject(template.EmailBody, appName);
                body = body.Replace("[AppName]", appName);
                body = body.Replace("[FullUserName]", name);
                body = body.Replace("[CompanyName]", companyName);
                body = body.Replace("{{site_url}}", url);
                body = body.Replace("{{action_url}}", callbackUrl);
                body = body.Replace("{{token_time}}", "24 hours");
                body = body.Replace("{{current_year}}", DateTime.Now.Year.ToString());
                List<string> data = new List<string>();
                data.Add(body);
                data.Add(LookupFromEmailAddress.FromEmailAddress);
                data.Add(emailSubject);
                return data;
            }
            catch (Exception ex)
            {

                throw;
            }
          

        }
        private string GetEmailSubject(string emailBody, string appName)
        {
            var title = GetTitle(emailBody);
            if (!string.IsNullOrWhiteSpace(title))
            {
                title = title.Replace("[AppName]", appName);
                return title = Regex.Replace(title, "<.*?>", String.Empty);
            }
            else
            {
                return title = "Reset Password";
            }
        }


        #endregion
    }

}


