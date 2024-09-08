using System;
using System.Threading.Tasks;
using System.Web.Http;
using PBASE.Entity;
using PBASE.Service;
using PBASE.WebAPI.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.Owin;
using System.Net.Http;
using System.Collections;

namespace PBASE.WebAPI.Controllers
{
    [Authorize]
    public class GridStateController : ApiController
    {
        #region Initialization
        private readonly ILookupService lookupService;
        private ApplicationUserManager _userManager = null;
        private ApplicationRoleManager _appRoleManager = null;
        public GridStateController(ILookupService _lookupService)
        {
            lookupService = _lookupService;
        }

        public GridStateController(ApplicationRoleManager appRoleManager
            , ApplicationUserManager userManager)
        {
            UserManager = userManager;
            AppRoleManager = appRoleManager;
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
        #endregion

        #region REST Operations

        [Route("api/GridState/Get")]
        [HttpGet]
        public async Task<IHttpActionResult> Get(string id)
        {
            try
            {
                int currentUserId = GetUserId();
                var internalGridSettingData = await lookupService.SelectMany_InternalGridSettingAsync(x => x.StorageKey == id && x.CreatedUserId == currentUserId);
                var internalGridSetting = internalGridSettingData.OrderByDescending(x=>x.InternalGridSettingId).FirstOrDefault();

                if (internalGridSetting == null)
                {
                    return Ok();
                }

                return Ok(internalGridSetting);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }


        }

        [HttpGet]
        [Route("api/GridState/GetSingle")]
        public async Task<IHttpActionResult> GetSingle(int? id)
        {
            try
            {
                int currentUserId = GetUserId();
                InternalGridSetting internalGridSetting = await lookupService.SelectSingle_InternalGridSettingAsync(x => x.InternalGridSettingId == id);

                if (internalGridSetting == null)
                {
                    return Ok();
                }
                InternalGridSettingDefault internalGridSettingDefault = await lookupService.SelectSingle_InternalGridSettingDefaultAsync(x => x.InternalGridSettingId == id && x.CreatedUserId == currentUserId);
                if(internalGridSettingDefault != null)
                {
                    internalGridSetting.IsDefalut = true;
                }
                else
                {
                    internalGridSetting.IsDefalut = false;
                }
                return Ok(internalGridSetting);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }


        }

        [HttpGet]
        [Route("api/GridState/GetSingleFirst")]
        public async Task<IHttpActionResult> GetSingleFirst(int? id)
        {
            try
            {
                int currentUserId = GetUserId();
                InternalGridSetting internalGridSetting = await lookupService.SelectSingle_InternalGridSettingAsync(x => x.InternalGridSettingId == id);

                if (internalGridSetting == null)
                {
                    return Ok();
                }
                var userRoles = lookupService.SelectSingle_vw_UserGrid(x => x.Id == currentUserId);
                var grisSettingUserRole = lookupService.SelectSingle_vw_UserGrid(x => x.Id == internalGridSetting.CreatedUserId);
                if (currentUserId != internalGridSetting.CreatedUserId && userRoles.AssignedRole == "User" && grisSettingUserRole.AssignedRole == "User")
                {
                    internalGridSetting.StorageData = null;
                }
            InternalGridSettingDefault internalGridSettingDefault = await lookupService.SelectSingle_InternalGridSettingDefaultAsync(x => x.StorageKey == internalGridSetting.StorageKey && x.CreatedUserId == currentUserId && x.InternalGridSettingId == id);

            if (internalGridSettingDefault != null)
            {
               internalGridSetting.IsDefalut = true;
            }
            return Ok(internalGridSetting);
         }
         catch (Exception ex)
         {
            return InternalServerError(ex);
         }


      }

      [HttpGet]
        [Route("api/GridState/GetDefaultFirst")]
        public async Task<IHttpActionResult> GetDefaultFirst(string id)
        {
            try
            {
                int currentUserId = GetUserId();
                InternalGridSettingDefault internalGridSettingDefault = await lookupService.SelectSingle_InternalGridSettingDefaultAsync(x => x.StorageKey == id && x.CreatedUserId == currentUserId);

                if (internalGridSettingDefault == null)
                {
                    return Ok();
                }
                InternalGridSetting internalGridSetting = await lookupService.SelectSingle_InternalGridSettingAsync(x => x.InternalGridSettingId == internalGridSettingDefault.InternalGridSettingId);
                if (internalGridSetting == null)
                {
                    return Ok();
                }
                var userRoles = lookupService.SelectSingle_vw_UserGrid(x => x.Id == currentUserId);
                var grisSettingUserRole = lookupService.SelectSingle_vw_UserGrid(x => x.Id == internalGridSetting.CreatedUserId);
                if (currentUserId != internalGridSetting.CreatedUserId && userRoles.AssignedRole == "User" && grisSettingUserRole.AssignedRole == "User")
                {
                    internalGridSetting.StorageData = null;
                }
                internalGridSetting.IsDefalut = true;
                return Ok(internalGridSetting);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }


        }

        [HttpGet]
        [Route("api/GridState/GetAll")]
        public IHttpActionResult GetAll(string id)
        {
            try
            {
                int currentUserId = GetUserId();
                var currentids = new List<int>();
                var vw_LookupSettingData = lookupService.SelectMany_vw_LookupSetting(x => x.UserId == currentUserId && x.StorageKey.Equals(id)).ToList()
                    .OrderBy(x=>x.SortOrder).ThenBy(x=>x.StateName).GroupBy(
                    p => p.SortOrder,
                    p =>  new { p.StateName,p.InternalGridSettingId,p.IsGlobal },
                    (key, g) => new { SortOrder = key, StateName = g.ToList() });

                return Ok(vw_LookupSettingData);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }


        }

        [Route("api/GridState/Save")]
        [HttpPost]
        public async Task<IHttpActionResult> Post(InternalGridSettingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            InternalGridSetting internalGridSetting;
            try
            {
                int currentUserId = GetUserId();
                InternalGridSetting _internalGridSetting = await lookupService.SelectSingle_InternalGridSettingAsync(x => x.StorageKey == model.StorageKey && x.StateName == model.StateName);
                if (_internalGridSetting == null)
                {
                    internalGridSetting = model.GetInternalGridSetting(null);
                    internalGridSetting.IsArchived = false;
                    int rowsEffected = await lookupService.SaveInternalGridSettingFormAsync(internalGridSetting);
                }
                else
                {
                    return BadRequest("State already exists with same name.");
                }
                if (model.IsDefault.Value) {
                    InternalGridSettingDefault internalGridSettingDefault = await lookupService.SelectSingle_InternalGridSettingDefaultAsync(x => x.StorageKey == model.StorageKey && x.CreatedUserId == currentUserId);

                    if (internalGridSettingDefault != null)
                    {
                        int rowsEffected = await lookupService.DeleteInternalGridSettingDefaultFormAsync(internalGridSettingDefault.InternalGridSettingDefaultId);
                    }
                    InternalGridSettingDefault internalGridSettingDefaultNewRecord = new InternalGridSettingDefault();
                    internalGridSettingDefaultNewRecord.InternalGridSettingDefaultId = 0;
                    internalGridSettingDefaultNewRecord.CreatedUserId = currentUserId;
                    internalGridSettingDefaultNewRecord.StorageKey = model.StorageKey;
                    internalGridSettingDefaultNewRecord.InternalGridSettingId = internalGridSetting.InternalGridSettingId;                  
                    internalGridSettingDefaultNewRecord.IsArchived = false;
                    await lookupService.SaveInternalGridSettingDefaultFormAsync(internalGridSettingDefaultNewRecord);
                }
                if (model.IsGlobal.Value) {
                    InternalGridSetting internalGridSettingForCheckBox;
                    internalGridSettingForCheckBox = await lookupService.SelectSingle_InternalGridSettingAsync(x => x.InternalGridSettingId == internalGridSetting.InternalGridSettingId);
                    if (internalGridSettingForCheckBox != null && internalGridSettingForCheckBox.IsGlobal != null)
                    {
                        if (internalGridSettingForCheckBox.IsGlobal.Value == true)
                        {
                            internalGridSettingForCheckBox.IsGlobal = false;
                        }
                        else
                        {
                            internalGridSettingForCheckBox.IsGlobal = true;

                        }
                    }
                    else
                    {
                        internalGridSettingForCheckBox.IsGlobal = true;
                    }

                    int rowsEffectedResult = await lookupService.SaveInternalGridSettingFormAsync(internalGridSetting);
                }
                return Ok(internalGridSetting);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("api/GridState/Update")]
        public async Task<IHttpActionResult> Update(InternalGridSettingViewModel model)
        {
            bool IsAllowed = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                int currentUserId = GetUserId();
                InternalGridSetting _internalGridSetting;
                if (CheckRole(currentUserId))
                {
                    _internalGridSetting = await lookupService.SelectSingle_InternalGridSettingAsync(x => x.InternalGridSettingId == model.StateId);
                }
                else
                {
                    _internalGridSetting = await lookupService.SelectSingle_InternalGridSettingAsync(x => x.InternalGridSettingId == model.StateId);
                    if (_internalGridSetting == null)
                    {
                        return Ok();
                    }
                }
                InternalGridSetting internalGridSetting = model.GetInternalGridSetting(_internalGridSetting);
                var userRoles = lookupService.SelectSingle_vw_UserGrid(x => x.Id == currentUserId);
                var grisSettingUserRole = lookupService.SelectSingle_vw_UserGrid(x => x.Id == internalGridSetting.CreatedUserId);
                if (userRoles.IsNotNull() && grisSettingUserRole.IsNotNull())
                {
                    if (userRoles.AssignedRole == "System Administrator")
                    {
                        IsAllowed = true;
                    }
                    else if (userRoles.AssignedRole == "Administrator" && (grisSettingUserRole.AssignedRole == "Administrator" || grisSettingUserRole.AssignedRole == "User"))
                    {
                        IsAllowed = true;
                    }
                    else if(userRoles.Id== grisSettingUserRole.Id)
                    {
                        IsAllowed = true;
                    }
                }
                if (IsAllowed == false)
                {
                    return BadRequest("User Not allowed to update Grid Setting");
                }
                int rowsEffected = await lookupService.SaveInternalGridSettingFormAsync(internalGridSetting);
                return Ok(internalGridSetting);

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/GridState/Delete")]
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                bool IsAllowed = false;
                int currentUserId = GetUserId();
                InternalGridSetting internalGridSetting;
                if (CheckRole(currentUserId))
                {
                    internalGridSetting = await lookupService.SelectSingle_InternalGridSettingAsync(x => x.InternalGridSettingId == id);
                }
                else
                {
                    internalGridSetting = await lookupService.SelectSingle_InternalGridSettingAsync(x => x.InternalGridSettingId == id);
                    if (internalGridSetting == null)
                    {
                        return Ok();
                    }
                }
                var userRoles = lookupService.SelectSingle_vw_UserGrid(x => x.Id == currentUserId);
                var grisSettingUserRole = lookupService.SelectSingle_vw_UserGrid(x => x.Id == internalGridSetting.CreatedUserId);
                if (userRoles.IsNotNull() && grisSettingUserRole.IsNotNull())
                {
                    if (userRoles.AssignedRole == "System Administrator")
                    {
                        IsAllowed = true;
                    }
                    else if (userRoles.AssignedRole == "Administrator" && (grisSettingUserRole.AssignedRole == "Administrator" || grisSettingUserRole.AssignedRole == "User"))
                    {
                        IsAllowed = true;
                    }
                    else if (userRoles.Id == grisSettingUserRole.Id)
                    {
                        IsAllowed = true;
                    }
                }
                if (IsAllowed == false)
                {
                    return BadRequest("User Not allowed to delete Grid Setting");
                }
                int rowsEffected = await lookupService.DeleteInternalGridSettingFormAsync(internalGridSetting.InternalGridSettingId);
                if (rowsEffected > 0)
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return InternalServerError();
        }

        [Route("api/GridState/DefaultGridSetting")]
        [HttpDelete]
        public async Task<IHttpActionResult> DefaultGridSetting(int id , string storageKey)
        {
         try
         {
            int currentUserId = GetUserId();
            InternalGridSettingDefault internalGridSettingDefault = await lookupService.SelectSingle_InternalGridSettingDefaultAsync(x => x.StorageKey == storageKey && x.CreatedUserId == currentUserId);

            if (internalGridSettingDefault != null)
            {
               if (internalGridSettingDefault.InternalGridSettingId == id)
               {
                  int rowsEffected = await lookupService.DeleteInternalGridSettingDefaultFormAsync(internalGridSettingDefault.InternalGridSettingDefaultId);
                  return Ok();
               }
               else
               {
                  int rowsEffected = await lookupService.DeleteInternalGridSettingDefaultFormAsync(internalGridSettingDefault.InternalGridSettingDefaultId);
               }
            }
            InternalGridSettingDefault internalGridSettingDefaultNewRecord = new InternalGridSettingDefault();
                internalGridSettingDefaultNewRecord.InternalGridSettingDefaultId = 0;
                internalGridSettingDefaultNewRecord.CreatedUserId = currentUserId;
                internalGridSettingDefaultNewRecord.StorageKey = storageKey;
                internalGridSettingDefaultNewRecord.InternalGridSettingId = id;
                internalGridSettingDefaultNewRecord.IsArchived = false;

                int rowsEffectedResult = await lookupService.SaveInternalGridSettingDefaultFormAsync(internalGridSettingDefaultNewRecord);

                if (rowsEffectedResult > 0)
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return InternalServerError();
        }

        [Route("api/GridState/GlobalSetting")]
        [HttpDelete]
        public async Task<IHttpActionResult> GlobalSetting(int id, string storageKey)
        {
            try
            {
                int currentUserId = GetUserId();
                InternalGridSetting internalGridSetting = await lookupService.SelectSingle_InternalGridSettingAsync(x => x.InternalGridSettingId == id);

                if (internalGridSetting != null && internalGridSetting.IsGlobal != null)
                {
                    if (internalGridSetting.IsGlobal.Value == true)
                    {
                        internalGridSetting.IsGlobal = false;
                    }
                    else
                    {
                        internalGridSetting.IsGlobal = true;

                    }
                }
                else {
                    internalGridSetting.IsGlobal = true;
                }

                int rowsEffectedResult = await lookupService.SaveInternalGridSettingFormAsync(internalGridSetting);

                if (rowsEffectedResult > 0)
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return InternalServerError();
        }

        protected int GetUserId()
        {
            return User.Identity.GetUserId<int>();
        }

        protected bool CheckRole(int id)
        {
            var userRoles = UserManager.GetRoles(id);
            if (userRoles.Contains("Administrator"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
