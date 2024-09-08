using Microsoft.AspNet.Identity.Owin;
using PBASE.Service;
using PBASE.WebAPI.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using PBASE.Entity;
using System.Web.Http;

namespace PBASE.WebAPI.Helpers
{
    public static class UserHelper
    {
        public static string GetRequestToken()
        {
            string token, cookieToken, formToken;
            AntiForgery.GetTokens(null, out cookieToken, out formToken);
            token = cookieToken + ":" + formToken;
            return token;
        }
        public async static Task<ApplicationUser> GetCurrentUser(int userId)
        {
            var user = CacheService.Get<ApplicationUser>(userId.ToString());
            if (user == null)
            {
                user = await SetCurrentUser(userId);
            }
            return user;
        }

        private async static Task<ApplicationUser> SetCurrentUser(int userId)
        {
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = await userManager.FindByIdAsync(userId);
            CacheService.Add(userId.ToString(), user);
            return user;
        }
        public static vw_UserGrid GetUserInfo(int userId)
        {
            var user = CacheService.Get<vw_UserGrid>(userId.ToString() + "userInfo");
            if (user == null)
            {
                user = SetUserInfo(userId);
            }
            return user;
        }

        private static vw_UserGrid SetUserInfo(int userId)
        {
            IUserLogService userLogService = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IUserLogService)) as IUserLogService;
            var userInfo = userLogService.SelectSingle_vw_UserGrid(x => x.Id == userId);
            CacheService.Add(userId.ToString() + "userInfo", userInfo);
            return userInfo;
        }

        public async static Task<List<Menu>> GetMenus(int userId)
        {
            List<Menu> permissions = new List<Menu>();
            permissions = CacheService.Get<List<Menu>>(userId.ToString() + "MenuOptions");
            if (permissions == null)
            {
                permissions = await SetMenus(userId);
            }
            return permissions;
        }

        private async static Task<List<Menu>> SetMenus(int userId)
        {
            var userService = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IUserService)) as IUserService;
            var agreementService = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IAgreementService)) as IAgreementService;
            List<Menu> permissions = new List<Menu>();
            var vw_UserMenuOption = await userService.SelectMany_vw_UserMenuOptionAsync(x => x.UserId == userId);
            permissions = vw_UserMenuOption.Select( x=> new Menu { Id = x.MenuOptionId, AccessTypeId = x.AccessTypeId }).ToList();
            var userAgreement = await agreementService.SelectMany_vw_UserAgreementFormAsync(x => x.UserId == userId);
            if (userAgreement.ToList().Any())
            {
                permissions.Clear();
                permissions.Add(new Menu { Id = 0, AccessTypeId = 0 });
            }
            CacheService.Add(userId.ToString() + "MenuOptions", permissions);
            return permissions; 
        }

        public async static Task<List<Menu>> GetDashboardItems(int userId)
        {
            List<Menu> dashboardItems = new List<Menu>();
            dashboardItems = CacheService.Get<List<Menu>>(userId.ToString() + "DashboardOptions");
            if (dashboardItems == null)
            {
                dashboardItems = await SetDashboardItems(userId);
            }
            return dashboardItems;
        }

        private async static Task<List<Menu>> SetDashboardItems(int userId)
        {
            var userService = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IUserService)) as IUserService;
            List<Menu> dashboardItems = new List<Menu>();
            var vw_UserDashboardOption = await userService.SelectMany_vw_UserDashboardOptionAsync(x => x.UserId == userId);
            dashboardItems = vw_UserDashboardOption.Select(x=> new Menu { Id= x.DashboardOptionId, AccessTypeId = 0 }).ToList();
            CacheService.Add(userId.ToString() + "DashboardOptions", dashboardItems);
            return dashboardItems;
        }

        public static void RemoveAllUserCache(int userId) {
            CacheService.Clear(userId.ToString());
            CacheService.Clear(userId.ToString() + "DashboardOptions");
            CacheService.Clear(userId.ToString() + "MenuOptions");
        }

        public static void RemoveAllCache()
        {
            CacheService.ClearAll();
        }
    }
}