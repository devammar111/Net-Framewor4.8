using PBASE.Service;
using System.Web.Http;
using PBASE.WebAPI.Controllers;
using PBASE.WebAPI.Helpers;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Reflection;
using PBASE.Entity;
using System;
using System.Linq;
using PBASE.WebAPI.ViewModels;
using System.IO;

namespace PBASE.WebAPI.Controllers
{
    public class UserController : BaseController
    {
        #region Initialization

        private readonly ILookupService lookupService;
        private readonly IUserService userService;
        private readonly IAgreementService agreementService;
        private readonly ISystemAlertService systemAlertService;

        public UserController(ILookupService lookupService, ISystemAlertService systemAlertService,
            IUserService userService, IAgreementService agreementService)
        {
            this.lookupService = lookupService;
            this.userService = userService;
            this.agreementService = agreementService;
	    this.systemAlertService = systemAlertService;
         }

        #endregion Initialization

        #region User

        public async Task<IHttpActionResult> Get()
        {
            int userId = GetUserId();
            LoggedUser user = new LoggedUser();
            user.IsAgree = false;
            user.User = await UserHelper.GetCurrentUser(userId);
            user.Menus = await UserHelper.GetMenus(userId);
            user.DashboardItems = await UserHelper.GetDashboardItems(userId);
            string connectString = ConfigurationManager.ConnectionStrings["AppContext"].ConnectionString;
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectString);
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                user.Database = connection.Database;
            }
            var userGrid = await lookupService.SelectSingle_vw_UserGridAsync(x => x.Id == userId);
            user.IsReadOnly = userGrid.IsReadOnly;
            user.IsDeleteDisabled = userGrid.IsDeleteDisabled;
            //Version Number
            var assembly = Assembly.GetExecutingAssembly();
            var version = assembly.GetName().Version;
            user.Version = version.ToString();
            user.LastUpdated = File.GetLastWriteTime(assembly.Location).ToString("dddd d MMM yyyy hh:mm tt");
            user.alerts = await systemAlertService.SelectAllvw_SystemAlertMessagessAsync(); //alert messages

            //User Profile Picture
            if (user.User.ProfileImageAttachmentId != null)
            {
                Attachment UserAttachment = await lookupService.SelectByAttachmentIdAsync(user.User.ProfileImageAttachmentId.Value);
                if (UserAttachment.IsNotNull())
                {
                    user.ProfileImageAttachmentFileHandle = UserAttachment.AttachmentFileHandle;
                }
            }

            return Ok(user);
        }

        #endregion

        [HttpGet]
        [Route("api/checkIsNotSystemAdmin")]
        public async Task<IHttpActionResult> GetUserType()
        {
            int userId = GetUserId();
            vw_LookupUsers vw_LookupUser = await lookupService.SelectSingle_vw_LookupUsersAsync(x => x.UserId == userId);
            if (vw_LookupUser.UserTypeId != 3)
            {
                return Ok("yes");
            }
            return Ok("no");
        }
    }
}
