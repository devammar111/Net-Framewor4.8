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
using PBASE.Entity.Helper;

namespace PBASE.WebAPI.Controllers
{
    [MenuPermission(Menus.SystemSettings)]
    [RoutePrefix("api")]
    public class UserGroupController : BaseController
    {
        #region Initialization

        private readonly ILookupService lookupService;
        private readonly IUserService userService;
        private ApplicationUserManager _userManager = null;

        public UserGroupController(ILookupService lookupService, IUserService userService
        )
        {
            this.lookupService = lookupService;
            this.userService = userService;
        }

        public UserGroupController(ApplicationUserManager userManager
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


        #region UserGroup

        [HttpGet]
        [Route("UserGroup/{id}")]
        [Decrypt("id")]
        public async Task<IHttpActionResult> GetUserGroup_UserGroupForm([FromUri]string id)
        {
            SystemSettings_UserGroupFormViewModel model = new SystemSettings_UserGroupFormViewModel();

            var allRoles = await lookupService.SelectAllvw_LookupRolesAsync();
            model.allRoles = id == "0" ? allRoles.Where(x => x.GroupBy == "ACTIVE") : allRoles;

            int UserGroupId = Convert.ToInt32(id.ToString());
            if (UserGroupId > 0)
            {
                try
                {
                    UserGroup userGroup = await userService.SelectByUserGroupIdAsync(UserGroupId);
                    base.CopyEntityToViewModel(userGroup, model);
                }
                catch (Exception)
                {
                    return InternalServerError();
                }
            }

            return Ok(model);
        }

        [HttpPost]
        [RequestValidation]
        [ReadOnlyValidation(Menus.SystemSettings)]
        [Route("UserGroup")]
        public async Task<IHttpActionResult> Post([FromBody]SystemSettings_UserGroupFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            UserGroup userGroup = new UserGroup();
            model.UserGroupId = 0;
            base.CopyViewModelToEntity(model, userGroup);

            int effectedRows = await userService.SaveUserGroupFormAsync(userGroup);
            if (effectedRows <= 0)
            {
                return Conflict();
            }
            var EncUserId = CryptoEngine.Encrypt(userGroup.UserGroupId.ToString());
            return Ok(EncUserId);
        }


        [HttpPut]
        [RequestValidation]
        [ReadOnlyValidation(Menus.SystemSettings)]
        [Route("UserGroup/{id}")]
        [Decrypt("id")]
        public async Task<IHttpActionResult> PutUserGroup_UserGroupForm([FromUri]string id, [FromBody]SystemSettings_UserGroupFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int effectedRows = 0;
            UserGroup userGroup = await userService.SelectByUserGroupIdAsync(model.UserGroupId.Value);
            CopyViewModelToEntity(model, userGroup);
            effectedRows = await userService.SaveUserGroupFormAsync(userGroup);
            if (effectedRows <= 0)
            {
                return NotFound();
            }
            UserHelper.RemoveAllCache();
            return Ok(true);

        }

        [HttpDelete]
        [ReadOnlyValidation(Menus.SystemSettings)]
        [Route("UserGroup/{id}")]
        [Decrypt("id")]
        public async Task<IHttpActionResult> DeleteUserGroupForm([FromUri]string id)
        {
            UserGroup UserGroup = await userService.SelectByUserGroupIdAsync(Convert.ToInt32(id));
            if (UserGroup == null)
            {
                return NotFound();
            }

            int effectedRows = await userService.DeleteUserGroupFormAsync(Convert.ToInt32(id));
            if (effectedRows <= 0)
            {
                return Conflict();
            }
            UserHelper.RemoveAllCache();
            return Ok(UserGroup);
        }

        #endregion

        #region UserGroupTable

        //GET: api/UserGroupTable/5
        [HttpGet]
        [Decrypt("id")]
        [Route("UserGroupMenuOptionTable/{id}")]
        public async Task<IHttpActionResult> Get([FromUri]string id)
        {
            var model = new UserGroupTableViewModel();
            var vw_LookupAccessTypes = await lookupService.SelectAllvw_LookupAccessTypesAsync();
            model.vw_LookupAccessType = vw_LookupAccessTypes.Select(x => new LookupEntity() { LookupId = x.AccessTypeId, LookupValue = x.AccessType, LookupExtraInt = x.LookupExtraInt, disabled = x.IsArchived, GroupBy = x.IsArchived.Value ? "ARCHIVED" : "ACTIVE" }).OrderBy(x => x.disabled).ThenBy(x => x.LookupValue);

            if (id != "0")
            {
                int userGroupId = Convert.ToInt32(id);
                var all_surveyFieldnames = await userService.SelectMany_vw_UserGroupObjectSubGridAsync(x => x.UserGroupId == userGroupId);
                model.Rows = all_surveyFieldnames.OrderBy(x => x.MenuName).ThenBy(x => x.TabId).ThenBy(x => x.SortOrder);
            }
        
            return Ok(model);
        }

        // PUT: api/templateSurveyDetailFormList/5 
        [HttpPut]
        [RequestValidation]
        [ReadOnlyValidation(Menus.SystemSettings)]
        [Route("UserGroupMenuOptionTable")]
        public async Task<IHttpActionResult> PutList(UserGroupTableViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (model.UpdatedRows.Count() > 0)
                {
                    int effectedRows = await userService.UserGroupMenuOptionTableActionsAsync(model.UpdatedRows);
                    if (effectedRows <= 0)
                    {
                        return InternalServerError();
                    }
                }
                
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
        #endregion

        #region UserGroupDashboardTable

        //GET: api/UserGroupDashboardOptionTable/5
        [HttpGet]
        [Decrypt("id")]
        [Route("UserGroupDashboardOptionTable/{id}")]
        public async Task<IHttpActionResult> GetDashboardOption([FromUri]string id)
        {
            var model = new UserGroupDashboardTableViewModel();
            var vw_LookupAccessTypes = await lookupService.SelectMany_vw_LookupAccessTypeAsync(x => x.LookupExtraInt == 2 || x.LookupExtraInt == 3);
            model.vw_LookupAccessType = vw_LookupAccessTypes.Select(x => new LookupEntity() { LookupId = x.AccessTypeId, LookupValue = x.AccessType, LookupExtraInt = x.LookupExtraInt, disabled = x.IsArchived, GroupBy = x.IsArchived.Value ? "ARCHIVED" : "ACTIVE" }).OrderBy(x => x.disabled).ThenBy(x => x.LookupValue);

            if (id != "0")
            {
                int userGroupId = Convert.ToInt32(id);
                var all_surveyFieldnames = await userService.SelectMany_vw_UserGroupDashboardObjectSubGridAsync(x => x.UserGroupId == userGroupId);
                model.Rows = all_surveyFieldnames.OrderBy(x => x.DashboardObject);
            }

            return Ok(model);
        }

        // PUT: api/UserGroupDashboardOptionTable/5 
        [HttpPut]
        [RequestValidation]
        [ReadOnlyValidation(Menus.SystemSettings)]
        [Route("UserGroupDashboardOptionTable")]
        public async Task<IHttpActionResult> PutListDashboardOption(UserGroupDashboardTableViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (model.UpdatedRows.Count() > 0)
                {
                    int effectedRows = await userService.UserGroupDashboardMenuOptionTableActionsAsync(model.UpdatedRows);
                    if (effectedRows <= 0)
                    {
                        return InternalServerError();
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
        #endregion
    }
}
