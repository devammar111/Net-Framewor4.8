using PBASE.Entity;
using PBASE.Service;
using System.Web.Http;
using System.Threading.Tasks;
using PBASE.WebAPI.Helpers;
using PBASE.WebAPI.ViewModels;
using System;

namespace PBASE.WebAPI.Controllers
{
    [MenuPermission(Menus.DashboardTC)]
    public class vw_UserAgreementFormController : BaseController
    {
        #region Initialization

       
        private readonly IAgreementService agreementService;

        public vw_UserAgreementFormController(IAgreementService agreementService)
        {
            this.agreementService = agreementService;
        }

        #endregion Initialization

        // GET: api/vw_EmailGrid/{GridSetting}
        [HttpGet]
        [Route("api/vw_UserAgreementForm")]
        public async Task<IHttpActionResult> vw_UserAgreementForm()
        {
            var userId = GetUserId();
            var agreements = await agreementService.SelectMany_vw_UserAgreementFormAsync(x=>x.UserId == userId);
            return Ok(agreements);
        }

        // POST: api/Template
        [HttpPost]
        [RequestValidation]
        [Route("api/vw_UserAgreementForm")]
        public async Task<IHttpActionResult> Post(UserAgreementViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var userId = GetUserId();
                UserAgreement agreement = new UserAgreement();
                ModelCopier.CopyModel(model, agreement);
                agreement.UserAgreementId = 0;
                agreement.AcceptDeclineDate = DateTime.Now;
                agreement.UserId = userId;
                int effectedRows = await agreementService.SaveUserAgreementFormAsync(agreement);
                if (effectedRows <= 0)
                {
                    return InternalServerError();
                }

                UserHelper.RemoveAllUserCache(userId);

                return Ok(agreement.UserAgreementId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}