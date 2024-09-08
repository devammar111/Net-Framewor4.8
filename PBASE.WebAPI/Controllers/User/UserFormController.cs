using System;
using System.Net;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Web.Http;
using System.Web.Http.Description;
using System.Threading.Tasks;
using Probase.GridHelper;
using System.Web.Http.ModelBinding;
using System.Collections.Generic;
using PBASE.Entity;
using PBASE.Service;
using PBASE.WebAPI.Helpers;
using PBASE.WebAPI.ViewModels;
using PBASE.Repository.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Net.Http;
using Microsoft.AspNet.Identity.Owin;
using System.IO;
using System.Web;
using System.Web.Hosting;
using System.Configuration;
using PBASE.Entity.Enum;
using PBASE.Entity.Helper;
using System.Text.RegularExpressions;

namespace PBASE.WebAPI.Controllers
{
    [MenuPermission(Menus.Users)]
    public partial class UserFormController : BaseController
    {
        #region Initialization

        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager = null;
        private ApplicationRoleManager _appRoleManager = null;
        private readonly ILookupService lookupService;
        private readonly IUserService userService;
        private readonly IEmailService emailService;
        private readonly IEmailTemplateService emailTemplateService;
        const int CREATE_PASSWORD = -9998; //Create password > EmailTemplateId=33

        public UserFormController(IUserService userService, ILookupService lookupService, IEmailService emailService, IEmailTemplateService emailTemplateService)
        {
            this.userService = userService;
            this.lookupService = lookupService;
            this.emailService = emailService;
            this.emailTemplateService = emailTemplateService;
        }

        public UserFormController(ApplicationUserManager userManager,
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


        #endregion Initialization

        [HttpGet]
        [Decrypt("id")]
        [Route("api/UserForm/{id}")]
        public async Task<IHttpActionResult> getUser([FromUri]string id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var userId = Convert.ToInt32(id);
                UserFormBindingModel model = new UserFormBindingModel();
                var allRoles = await lookupService.SelectAllvw_LookupRolesAsync();
                model.vw_lookuprole = id == "0" ? allRoles.Where(x => x.GroupBy == "ACTIVE") : allRoles;
                
                var vw_lookupusergroups = await lookupService.SelectAllvw_LookupUserGroupsAsync();
                model.vw_lookupusergroups = id == "0" ? vw_lookupusergroups.Where(x => x.GroupBy == "ACTIVE") : vw_lookupusergroups;

                if (id != "0")
                {
                    var user = await UserManager.FindByIdAsync(userId);
                    if (user == null)
                    {
                        return NotFound();
                    }
                    var role = await UserManager.GetRolesAsync(userId);
                    user.CopyTo(model);
                }
                
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        [HttpPost]
        [RequestValidation]
        [ReadOnlyValidation(Menus.Users)]
        [Route("api/UserForm")]
        public async Task<IHttpActionResult> CreateUser(UserFormBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int currentUserId = base.GetUserId();
            DateTime currentDateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "UTC", "GMT Standard Time");
            Guid guid = Guid.NewGuid();
            ApplicationUser user = new ApplicationUser();
            user.CopyFrom(model);
            user.CreatedUserId = currentUserId;
            user.CreatedDate = currentDateTime;
            IdentityResult result = await UserManager.CreateAsync(user);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            var ApplicationInformation = emailService.SelectAllApplicationInformations();
            ApplicationInformation app_info = ApplicationInformation.ToList()[0];
            string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
            try
            {
                var EncUserId = CryptoEngine.Encrypt(user.Id.ToString());
                var callbackUrl = ConfigurationManager.AppSettings["PBASEUrl"].ToString() + "/authentication/change-password?id=" + EncUserId + "&password=Set&FormType=CreateUser" + "&verificationKey=" + HttpUtility.UrlEncode(code);
                IdentityMessage message = new IdentityMessage();
                var Data = GenerateEmailBody(CREATE_PASSWORD, callbackUrl, app_info.ApplicationName, ConfigurationManager.AppSettings["PBASEUrl"].ToString(), user.FirstName + " " + user.LastName, app_info.CompanyName);
                message.Body = Data[0];
                message.Destination = model.Email;
                message.Subject = Data[2];
                SaveEmailToDatabase(Data[1].ToString(), message);

                return Ok(user.Id);
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        [HttpPut]
        [RequestValidation]
        [ReadOnlyValidation(Menus.Users)]
        [Decrypt("id")]
        [Route("api/UserForm/{id}")]
        public async Task<IHttpActionResult> UpdateUser([FromUri]string id, [FromBody]UserFormBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int currentUserId = base.GetUserId();
            DateTime currentDateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "UTC", "GMT Standard Time");
            int userId = Convert.ToInt32(id);
            var user = await UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            user.CopyFrom(model);
            user.UpdatedUserId = currentUserId;
            user.UpdatedDate = currentDateTime;
            IdentityResult result = await UserManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }
            
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }
            UserHelper.RemoveAllUserCache(userId);
            return Ok(model);
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

        private List<string> GenerateEmailBody(int EmailTemplateTypeId, string callbackUrl, string appName, string url, string name, string companyName)
        {
            string body = string.Empty;
            var template = emailTemplateService.SelectSingle_EmailTemplate(x => x.EmailTemplateTypeId == EmailTemplateTypeId);
            vw_LookupFromEmailAddress LookupFromEmailAddress = lookupService.SelectSingle_vw_LookupFromEmailAddress(x => x.FromEmailAddressId == template.FromEmailAddressId);
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

        private string GenerateResetEmailBody(string callbackUrl, string template, string url, string name, string thirdPartyName, string appName = "PBASE")
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
            body = body.Replace("{{third_party_name}}", thirdPartyName);
            body = body.Replace("{{token_time}}", "24 hours");
            body = body.Replace("{{action_url}}", callbackUrl);
            body = body.Replace("{{operating_system}}", "[ " + UserAgent + " ]");
            body = body.Replace("{{browser_name}}", "[ " + bc.Browser + " " + bc.Version + " ]");
            body = body.Replace("{{current_year}}", DateTime.Now.Year.ToString());
            body = body.Replace("{{app_name}}", appName);
            return body;
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
            emailService.SaveEmailForm(email);

            return email.EmailId.Value;
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
    }
}

