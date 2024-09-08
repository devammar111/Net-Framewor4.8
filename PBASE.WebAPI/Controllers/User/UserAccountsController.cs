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
    [RoutePrefix("api/lookup")]
    [MenuPermission(Menus.Users)]
    public partial class UserAccountsController : BaseController
    {
        #region Initialization

        private readonly IUserService userService;

        public UserAccountsController(IUserService userService
        )
        {
            this.userService = userService;
        }

        #endregion Initialization

        [HttpGet]
        [Route("UserAccounts/{id}")]
        public async Task<IHttpActionResult> GetUserAccounts([FromUri]int id)
        {
            var model = new UserAccountsViewModel();
            UserAccounts UserAccounts = await userService.SelectByUserAccountsIdAsync(id);
            if (UserAccounts == null)
            {
                return NotFound();
            }

            ModelCopier.CopyModel(UserAccounts, model);
            return Ok(model);
        }

        [HttpPut]
        [RequestValidation]
        [ReadOnlyValidation(Menus.Users)]
        [Route("UserAccounts/{id}")]
        public async Task<IHttpActionResult> UserAccounts([FromUri]int id, UserAccountsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var UserAccounts = await userService.SelectByUserAccountsIdAsync(id);
            if (UserAccounts != null)
            {
                ModelCopier.CopyModel(model, UserAccounts);
            }
            int effectedRows = await userService.SaveUserAccountsFormAsync(UserAccounts);
            if (effectedRows <= 0)
            {
                return Conflict();
            }

            return Ok();
        }

        [HttpPost]
        [RequestValidation]
        [ReadOnlyValidation(Menus.Users)]
        [Route("UserAccounts")]
        public async Task<IHttpActionResult> UserAccounts(UserAccountsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var UserAccounts = new UserAccounts();
            ModelCopier.CopyModel(model, UserAccounts);
            int effectedRows = await userService.SaveUserAccountsFormAsync(UserAccounts);
            if (effectedRows <= 0)
            {
                return NotFound();
            }
            return Ok(UserAccounts);
        }


        [HttpDelete]
        [ReadOnlyValidation(Menus.Users)]
        [Route("UserAccounts/{id}")]
        public async Task<IHttpActionResult> DeleteUserAccounts(int id)
        {
            UserAccounts UserAccounts = await userService.SelectByUserAccountsIdAsync(id);
            if (UserAccounts == null)
            {
                return NotFound();
            }

            int effectedRows = await userService.DeleteUserAccountsFormAsync(id);
            if (effectedRows <= 0)
            {
                return BadRequest(userService.LastErrorMessage);
            }
            return Ok();
        }
    }
}