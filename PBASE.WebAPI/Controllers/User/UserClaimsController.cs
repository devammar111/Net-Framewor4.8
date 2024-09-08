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

namespace PBASE.WebAPI.Controllers
{
    [MenuPermission(Menus.Users)]
    [RoutePrefix("api/lookup")]
    public partial class UserClaimsController : BaseController
    {
        #region Initialization
        private readonly IUserService UserService;
        public UserClaimsController(IUserService UserService
        )
        {
            this.UserService = UserService;
        }
        #endregion Initialization
        [HttpGet]
        [Route("UserClaims/{id}")]
        public async Task<IHttpActionResult> GetUserClaims([FromUri]int id)
        {
            var model = new UserClaimsViewModel();
            UserClaims UserClaims = await UserService.SelectByUserClaimsIdAsync(id);
            if (UserClaims == null)
            {
                return NotFound();
            }
            ModelCopier.CopyModel(UserClaims, model);
            return Ok(model);
        }
        [HttpPut]
        [RequestValidation]
        [ReadOnlyValidation(Menus.Users)]
        [Route("UserClaims/{id}")]
        public async Task<IHttpActionResult> UserClaims([FromUri]int id, UserClaimsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var UserClaims = await UserService.SelectByUserClaimsIdAsync(id);
            if (UserClaims != null)
            {
                ModelCopier.CopyModel(model, UserClaims);
            }
            int effectedRows = await UserService.SaveUserClaimsFormAsync(UserClaims);
            if (effectedRows <= 0)
            {
                return Conflict();
            }
            return Ok();
        }
        [HttpPost]
        [RequestValidation]
        [ReadOnlyValidation(Menus.Users)]
        [Route("UserClaims")]
        public async Task<IHttpActionResult> UserClaims(UserClaimsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var UserClaims = new UserClaims();
            ModelCopier.CopyModel(model, UserClaims);
            int effectedRows = await UserService.SaveUserClaimsFormAsync(UserClaims);
            if (effectedRows <= 0)
            {
                return NotFound();
            }
            return Ok(UserClaims);
        }
        [HttpDelete]
        [ReadOnlyValidation(Menus.Users)]
        [Route("UserClaims/{id}")]
        public async Task<IHttpActionResult> DeleteUserClaims(int id)
        {
            UserClaims UserClaims = await UserService.SelectByUserClaimsIdAsync(id);
            if (UserClaims == null)
            {
                return NotFound();
            }
            int effectedRows = await UserService.DeleteUserClaimsFormAsync(id);
            if (effectedRows <= 0)
            {
                return BadRequest(UserService.LastErrorMessage);
            }
            return Ok();
        }
    }
}