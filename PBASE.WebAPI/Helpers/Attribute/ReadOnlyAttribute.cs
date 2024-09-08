using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Microsoft.AspNet.Identity;
using PBASE.Service;

namespace PBASE.WebAPI.Helpers
{
    public class ReadOnlyValidation : ActionFilterAttribute
    {
        private static readonly IUserLogService userLogService = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IUserLogService)) as IUserLogService;
        private int _claimValue;
        public ReadOnlyValidation(Menus claimValue)
        {
            _claimValue = (int)claimValue;
        }
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            HttpContext ctx = HttpContext.Current;
            int userId = HttpContext.Current.User.Identity.GetUserId<int>();
            if (userId.IsNotNull())
            {
                var userInfo = UserHelper.GetUserInfo(userId);
                var action = actionContext.Request.Method.Method;
                var task = Task.Run(async () => await UserHelper.GetMenus(userId));
                var menu = task.Result.Where(x => x.Id == _claimValue).FirstOrDefault();
                if ((userInfo.IsNotNull() && userInfo.IsReadOnly == true) && (action == "POST" || action == "PUT" || action == "DELETE"))
                {
                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadGateway, "You have Read Only access.");
                    return;
                }
                if ((menu.IsNotNull() && menu.AccessTypeId == -9968) && (action == "POST" || action == "PUT" || action == "DELETE"))
                {
                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadGateway, "You have Read Only access to this menu.");
                    return;
                }
                if (((userInfo.IsNotNull() && userInfo.IsDeleteDisabled == true) || (menu.IsNotNull() && menu.AccessTypeId == -9967)) && action == "DELETE")
                {
                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadGateway, "You don't have permission to delete.");
                    return;
                }
            }
            else
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadGateway);
                return;
            }

            base.OnActionExecuting(actionContext);
        }

    }
}
