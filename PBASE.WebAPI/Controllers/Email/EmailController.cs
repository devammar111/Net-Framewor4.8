using System;
using System.Linq;
using System.Web.Http;
using System.Threading.Tasks;
using PBASE.Service;
using PBASE.WebAPI.ViewModels;
using PBASE.WebAPI.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Net.Http;
using Microsoft.AspNet.Identity.Owin;
using PBASE.Entity;

namespace PBASE.WebAPI.Controllers
{
    [MenuPermission(Menus.Email)]
    public class EmailController : BaseController
    {
        #region Initialization

        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager = null;
        private ApplicationRoleManager _appRoleManager = null;
        private readonly ILookupService lookupService;
        private readonly IUserService userService;
        private readonly IEmailService emailService;

        public EmailController(IUserService userService, ILookupService lookupService, IEmailService emailService)
        {
            this.userService = userService;
            this.lookupService = lookupService;
            this.emailService = emailService;
        }

        public EmailController(ApplicationUserManager userManager,
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

        // GET: api/Email/5
        [HttpGet]
        [Decrypt("id")]
        [Route("api/Email/{id}")]
        public async Task<IHttpActionResult> Get([FromUri]string id)
        {
            var model = new EmailViewModel();
            if (id != "0")
            {
                Email email = await emailService.SelectByEmailIdAsync(Convert.ToInt32(id));
                var user = await UserManager.FindByIdAsync(email.CreatedUserId);
                model.UserFullName = user.FirstName + ' ' + user.LastName;

                var emailType = lookupService.SelectSingle_vw_LookupEmailType(x => x.EmailTypeId == email.EmailTypeId);
                var emailAttchment = emailService.SelectMany_EmailAttachment(x => x.EmailId == email.EmailId);
                var attchments = lookupService.SelectMany_Attachment(x => emailAttchment.Select(y => y.AttachmentId).Contains(x.AttachmentId));
                model.EmailType = emailType.EmailType;
                model.EmailAttachments = attchments.ToList();
                CopyEntityToViewModel(email, model);
                if (email == null)
                {
                    return NotFound();
                }

            }

            return Ok(model);
        }

        // POST: api/Email
        [HttpPost]
        [ReadOnlyValidation(Menus.Email)]
        [Route("api/Email")]
        public async Task<IHttpActionResult> Post(EmailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Email email = await emailService.SelectByEmailIdAsync(model.EmailId.Value);

            email.SentDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "UTC", "GMT Standard Time");
            email.Status = "Sent";
            int effectedRows = await emailService.SaveEmailFormAsync(email);
            IdentityMessage message = new IdentityMessage();
            message.Subject = email.Subject;
            message.Body = email.Body;
            message.Destination = email.ToAddress;
            EmailServiceHelper emailServiceHelper = new EmailServiceHelper();
            await emailServiceHelper.SendAsync(message);

            return Ok(email.EmailId);
        }

    }
}