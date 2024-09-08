using PBASE.Entity;
using PBASE.Entity.Enum;
using PBASE.Service;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using PBASE.WebAPI.Controllers;
using PBASE.WebAPI.ViewModels;
using System.Web.Configuration;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using PBASE.WebAPI.Helpers;
using PBASE.WebAPI;
using System.Net.Http;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;

namespace PBASE.WebAPI.Controllers
{
    [MenuPermission(Menus.SystemSettings)]
    [RoutePrefix("api")]
    public class UserDashboardOptionRoleController : BaseController
    {
        #region Initialization

        private readonly ILookupService lookupService;
        private readonly IUserService userService;
        private ApplicationUserManager _userManager = null;

        public UserDashboardOptionRoleController(ILookupService lookupService, IUserService userService
        )
        {
            this.lookupService = lookupService;
            this.userService = userService;
        }

        public UserDashboardOptionRoleController(ApplicationUserManager userManager
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


        #region UserDashboardOptionRole

        [HttpGet]
        [Route("UserDashboardOptionRole/{id}")]
        [Decrypt("id")]
        public async Task<IHttpActionResult> GetAsync([FromUri]string id)
        {
            SystemSettings_UserDashboardOptionRoleViewModel model = new SystemSettings_UserDashboardOptionRoleViewModel();
            var allRoles = await lookupService.SelectAllvw_LookupRolesAsync();
            model.allRoles = id == "0" ? allRoles.Where(x => x.GroupBy == "ACTIVE") : allRoles;

            int DashboardOptionId = Convert.ToInt32(id.ToString());
            if (DashboardOptionId != 0)
            {
                try
                {
                    UserDashboardOptionRole UserDashboardOptionRole = new UserDashboardOptionRole();
                    model.AspNetRoleIds = userService.SelectMany_UserDashboardOptionRole(x => x.DashboardOptionId == DashboardOptionId).Select(x => x.AspNetRoleId.Value).ToList();
                    base.CopyEntityToViewModel(UserDashboardOptionRole, model);
                    model.DashboardOptionId = DashboardOptionId;
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }

            return Ok(model);
        }

        [RequestValidation]
        [ReadOnlyValidation(Menus.SystemSettings)]
        [HttpPut]       
        [Route("UserDashboardOptionRole/{id}")]
        [Decrypt("id")]
        public IHttpActionResult Put([FromUri]string id, [FromBody]SystemSettings_UserDashboardOptionRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            UserDashboardOptionRole userMenuOptionRole = new UserDashboardOptionRole();
            base.CopyViewModelToEntity(model, userMenuOptionRole);
            var result = userService.SaveRoleFormWithUserDashboardOptionRole(userMenuOptionRole, model.AspNetRoleIds);
            if (result == false)
            {
                return BadRequest(userService.LastErrorMessage);
            }
            return Ok(result);

        }

        [HttpDelete]
        [ReadOnlyValidation(Menus.SystemSettings)]
        [Route("UserDashboardOptionRole/{id}")]
        [Decrypt("id")]
        public async Task<IHttpActionResult> Delete([FromUri]string id)
        {
            UserDashboardOptionRole UserDashboardOptionRole = await userService.SelectByUserDashboardOptionRoleIdAsync(Convert.ToInt32(id));
            if (UserDashboardOptionRole == null)
            {
                return NotFound();
            }

            int effectedRows = await userService.DeleteUserDashboardOptionRoleFormAsync(Convert.ToInt32(id));
            if (effectedRows <= 0)
            {
                return Conflict();
            }

            return Ok(UserDashboardOptionRole);
        }

        #endregion
    }
}
