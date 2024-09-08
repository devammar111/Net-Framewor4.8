using System;
using System.Linq;
using System.Web.Http;
using System.Threading.Tasks;
using Probase.GridHelper;
using PBASE.Entity;
using PBASE.Service;
using PBASE.WebAPI.ViewModels;
using PBASE.WebAPI.Helpers;

namespace PBASE.WebAPI.Controllers
{
    [MenuPermission(Menus.Agreements)]
    public class UserAgreementController : BaseController
    {
        #region Initialization

        private readonly ILookupService lookupService;
        private readonly IAgreementService agreementService;

        public UserAgreementController(ILookupService lookupService, IAgreementService agreementService
        )
        {
            this.lookupService = lookupService;
            this.agreementService = agreementService;
        }

        #endregion Initialization

        // GET: api/Template/5
        [HttpGet]
        [Decrypt("id")]
        [Route("api/UserAgreement/{id}")]
        public async Task<IHttpActionResult> Get([FromUri]string id)
        {
            var model = new UserAgreementViewModel();

            if (id != "0")
            {
                UserAgreement userAgreement = await agreementService.SelectByUserAgreementIdAsync(Convert.ToInt32(id));
                CopyEntityToViewModel(userAgreement, model);
                if (userAgreement == null)
                {
                    return NotFound();
                }
                
            }

            return Ok(model);
        }

        // POST: api/Template
        [HttpPost]
        [ReadOnlyValidation(Menus.Agreements)]
        [RequestValidation]
        [Route("api/UserAgreement")]
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
                    return NotFound();
                }

                return Ok(agreement.UserAgreementId);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        // PUT: api/Template/5 
        [HttpPut]
        [ReadOnlyValidation(Menus.Agreements)]
        [RequestValidation]
        [Route("api/UserAgreement/{id}")]
        [Decrypt("id")]
        public async Task<IHttpActionResult> Put([FromUri]string id, UserAgreementViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                UserAgreement agreement = await agreementService.SelectByUserAgreementIdAsync(Convert.ToInt32(id));
                CopyViewModelToEntity(model, agreement);

                int effectedRows = await agreementService.SaveUserAgreementFormAsync(agreement);
                if (effectedRows <= 0)
                {
                    return Conflict();
                }

                return Ok("Data has been updated Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        // DELETE: api/Template/5
        [HttpDelete]
        [ReadOnlyValidation(Menus.Agreements)]
        [Route("api/UserAgreement/{id}")]
        [Decrypt("id")]
        public async Task<IHttpActionResult> Delete([FromUri]string id)
        {
            UserAgreement agreement = await agreementService.SelectByUserAgreementIdAsync(Convert.ToInt32(id));
            if (agreement == null)
            {
                return NotFound();
            }
            int effectedRows = await agreementService.DeleteUserAgreementFormAsync(agreement.UserAgreementId.Value);
            if (effectedRows <= 0)
            {
                return NotFound();
            }
            return Ok(agreement.UserAgreementId.Value);
        }
    }
}