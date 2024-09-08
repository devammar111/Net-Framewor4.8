using PBASE.Entity;
using PBASE.Service;
using System;
using System.Linq;
using System.Web.Http;
using PBASE.WebAPI.ViewModels;
using System.Threading.Tasks;
using PBASE.WebAPI.Helpers;
using System.Net.Http;
using Microsoft.AspNet.Identity.Owin;

namespace PBASE.WebAPI.Controllers
{
    [MenuPermission(Menus.SystemSettings)]
    [RoutePrefix("api")]
    public class UserMenuOptionRoleController : BaseController
    {
        #region Initialization

        private readonly ILookupService lookupService;
        private readonly IUserService userService;
        private ApplicationUserManager _userManager = null;

        public UserMenuOptionRoleController(ILookupService lookupService, IUserService userService
        )
        {
            this.lookupService = lookupService;
            this.userService = userService;
        }

        public UserMenuOptionRoleController(ApplicationUserManager userManager
            )
        {
            UserManager = userManager;
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

        #endregion Initialization


        #region UserMenuOptionRole

        [HttpGet]
        [Route("UserMenuOptionRole/{id}")]
        [Decrypt("id")]
        public async Task<IHttpActionResult> GetAsync([FromUri]string id)
        {
            SystemSettings_UserMenuOptionRoleViewModel model = new SystemSettings_UserMenuOptionRoleViewModel();

            var allRoles = await lookupService.SelectAllvw_LookupRolesAsync();
            model.allRoles = id == "0" ? allRoles.Where(x => x.GroupBy == "ACTIVE") : allRoles;

            int MenuOptionId = Convert.ToInt32(id.ToString());
            if (MenuOptionId != 0)
            {
                try
                {
                    UserMenuOptionRole UserMenuOptionRole = new UserMenuOptionRole();
                    model.AspNetRoleIds = userService.SelectMany_UserMenuOptionRole(x => x.MenuOptionId == MenuOptionId).Select(x => x.AspNetRoleId.Value).ToList();
                    base.CopyEntityToViewModel(UserMenuOptionRole, model);
                    model.MenuOptionId = MenuOptionId;
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }

            return Ok(model);
        }

        [HttpPost]
        [RequestValidation]
        [ReadOnlyValidation(Menus.SystemSettings)]
        [Route("UserMenuOptionRole")]
        public async Task<IHttpActionResult> Post([FromBody]SystemSettings_UserMenuOptionRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            UserMenuOptionRole userMenuOptionRole = new UserMenuOptionRole();
            model.UserMenuOptionRoleId = 0;
            base.CopyViewModelToEntity(model, userMenuOptionRole);
            
            int effectedRows = await userService.SaveUserMenuOptionRoleFormAsync(userMenuOptionRole);
            if(effectedRows <= 0)
            {
                return Conflict();
            }
            else
            {
                var result = userService.SaveRoleFormWithUserMenuOptionRole(userMenuOptionRole, model.AspNetRoleIds);
                if (result == false)
                {
                    return BadRequest(userService.LastErrorMessage);
                }
                UserHelper.RemoveAllCache();
            }

            return Ok(userMenuOptionRole);
        }


        [HttpPut]
        [RequestValidation]
        [ReadOnlyValidation(Menus.SystemSettings)]
        [Route("UserMenuOptionRole/{id}")]
        [Decrypt("id")]
        public async Task<IHttpActionResult> Put([FromUri]string id, [FromBody]SystemSettings_UserMenuOptionRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //UserMenuOptionRole UserMenuOptionRole = await userService.SelectByUserMenuOptionRoleIdAsync(Convert.ToInt32(id));
            //if (UserMenuOptionRole == null)
            //{
            //    return NotFound();
            //}

            UserMenuOptionRole userMenuOptionRole = new UserMenuOptionRole();
            base.CopyViewModelToEntity(model, userMenuOptionRole);
            var result = userService.SaveRoleFormWithUserMenuOptionRole(userMenuOptionRole, model.AspNetRoleIds);
            if (result == false)
            {
                return BadRequest(userService.LastErrorMessage);
            }
            UserHelper.RemoveAllCache();
            return Ok(result);
        }

        [HttpDelete]
        [ReadOnlyValidation(Menus.SystemSettings)]
        [Route("UserMenuOptionRole/{id}")]
        [Decrypt("id")]
        public async Task<IHttpActionResult> Delete([FromUri]string id)
        {
            UserMenuOptionRole UserMenuOptionRole = await userService.SelectByUserMenuOptionRoleIdAsync(Convert.ToInt32(id));
            if (UserMenuOptionRole == null)
            {
                return NotFound();
            }

            int effectedRows = await userService.DeleteUserMenuOptionRoleFormAsync(Convert.ToInt32(id));
            if (effectedRows <= 0)
            {
                return Conflict();
            }
            UserHelper.RemoveAllCache();
            return Ok(UserMenuOptionRole);
        }

        #endregion
    }
}
