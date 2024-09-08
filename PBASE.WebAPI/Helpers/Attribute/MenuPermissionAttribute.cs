using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace PBASE.WebAPI.Helpers
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]

    public class MenuPermissionAttribute : AuthorizeAttribute
    {
        private int _claimValue;
        public MenuPermissionAttribute(Menus claimValue)
        {
            _claimValue = (int)claimValue;
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var context = (HttpContextBase)actionContext.Request.Properties["MS_HttpContext"];
            var userId = context.User.Identity.GetUserId<int>();
            var task = Task.Run(async () => await UserHelper.GetMenus(userId));
            var menu = task.Result.Where(x => x.Id == _claimValue).FirstOrDefault();
            if (menu.IsNotNull())
            {
                if (menu.AccessTypeId.IsNotNull())
                {
                    return true;
                }
            }
            return false;
        }
    }

    public enum Menus
    {
        Dashboard = -9990,
        Templates = -9989,
        Lookups = -9988,
        Users = -9987,
        LoginAnalysis = -9986,
        SystemSettings = -9985,
        ExportLog = -9984,
        Email = -9981,
        EmailTemplate = -9982,
        SystemAdministration = -9945,
        Agreements = -9978,
        SystemAlerts = -9979,
        Test = -9962,
        DashboardTC = 0,
        InvalidEmailLog = 1004
    }
}