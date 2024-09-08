using System;
using System.Linq;
using System.Web.Http;
using System.Threading.Tasks;
using PBASE.Entity;
using PBASE.Service;
using PBASE.WebAPI.ViewModels;
using PBASE.WebAPI.Helpers;
using System.Web;
using Microsoft.AspNet.Identity;
using PBASE.Entity.Helper;

namespace PBASE.WebAPI.Controllers
{
    [MenuPermission(Menus.Agreements)]
    public class AgreementController : BaseController
    {
        #region Initialization

        private readonly ILookupService lookupService;
        private readonly IAgreementService agreementService;

        public AgreementController(ILookupService lookupService, IAgreementService agreementService
        )
        {
            this.lookupService = lookupService;
            this.agreementService = agreementService;
        }

        #endregion Initialization

        // GET: api/Template/5
        [HttpGet]
        [Decrypt("id")]
        [Route("api/Agreement/{id}")]
        public async Task<IHttpActionResult> Get([FromUri]string id)
        {
            var model = new AgreementViewModel();
            var vw_LookupRoles = await lookupService.SelectAllvw_LookupRolesAsync();
            model.vw_lookupRole = id == "0" ? vw_LookupRoles.Where(x => x.GroupBy == "ACTIVE") : vw_LookupRoles;

            if (id != "0")
            {
                Agreement Agreement = await agreementService.SelectByAgreementIdAsync(Convert.ToInt32(id));
                CopyEntityToViewModel(Agreement, model);
                if (Agreement == null)
                {
                    return NotFound();
                }

                if (Agreement.AgreementId != null)
                {
                    model.AgreementUserTypeIds = agreementService.SelectMany_AgreementUserType(x => x.AgreementId == Agreement.AgreementId.Value).Select(x => x.UserTypeId.Value).ToList();
                }
            }

            return Ok(model);
        }
        [HttpGet]
        [Decrypt("id")]
        [Route("api/sp_AgreementVersionNew/{id}")]
        public async Task<IHttpActionResult> sp_AgreementVersionNew([FromUri]string id)
        {
            int agreementId = Convert.ToInt32(id);
            string agreementEncrypt = string.Empty;
            int? newAgreementId = 0;
            bool? isLocked = null;
            int userId = HttpContext.Current.User.Identity.GetUserId<int>();
            newAgreementId = agreementService.sp_AgreementVersionNew(userId, agreementId, ref newAgreementId);
            agreementEncrypt = CryptoEngine.Encrypt(newAgreementId.ToString());
            vw_AgreementGrid vw_AgreementGrid = await agreementService.SelectSingle_vw_AgreementGridAsync(x => x.AgreementId == newAgreementId);
            if (vw_AgreementGrid != null) {
                isLocked = vw_AgreementGrid.IsLocked;
            }
            var result = new
            {
                IsLocked = isLocked,
                AgreementId = agreementEncrypt
            };
            return Ok(result);
        }
        // POST: api/Template
        [HttpPost]
        [ReadOnlyValidation(Menus.Agreements)]
        [RequestValidation]
        [Route("api/Agreement")]
        public async Task<IHttpActionResult> Post(AgreementViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Agreement agreement = new Agreement();
                ModelCopier.CopyModel(model, agreement);
                agreement.AgreementId = 0;
                int effectedRows = await agreementService.SaveAgreementFormAsync(agreement);
                if (effectedRows <= 0)
                {
                    return NotFound();
                }

                var result = agreementService.SaveMultiAgreementUserType(agreement, model.AgreementUserTypeIds);
                if (result == false)
                {
                    return BadRequest(agreementService.LastErrorMessage);
                }

                return Ok(agreement.AgreementId);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        // PUT: api/Template/5 
        [HttpPut]
        [RequestValidation]
        [ReadOnlyValidation(Menus.Agreements)]
        [Route("api/Agreement/{id}")]
        [Decrypt("id")]
        public async Task<IHttpActionResult> Put([FromUri]string id, AgreementViewModel model)
        {
            try
            {
                int agreementId = Convert.ToInt32(id);
                vw_AgreementPreviousVersionNumber vw_AgreementPreviousVersionNumber = agreementService.SelectSingle_vw_AgreementPreviousVersionNumber(x => x.AgreementId == agreementId);
                if (vw_AgreementPreviousVersionNumber!=null && model.VersionNo.Value <= vw_AgreementPreviousVersionNumber.VersionNo.Value)
                {
                    ModelState.AddModelError("", "Version No entered needs to be greater than " + vw_AgreementPreviousVersionNumber.VersionNo.Value);
                    return BadRequest(ModelState);

                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                Agreement agreement = await agreementService.SelectByAgreementIdAsync(Convert.ToInt32(id));
                CopyViewModelToEntity(model, agreement);

                int effectedRows = await agreementService.SaveAgreementFormAsync(agreement);
                if (effectedRows <= 0)
                {
                    return Conflict();
                }

                var result = agreementService.SaveMultiAgreementUserType(agreement, model.AgreementUserTypeIds);
                if (result == false)
                {
                    return BadRequest(agreementService.LastErrorMessage);
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
        [Route("api/Agreement/{id}")]
        [Decrypt("id")]
        public async Task<IHttpActionResult> Delete([FromUri]string id)
        {
            Agreement agreement = await agreementService.SelectByAgreementIdAsync(Convert.ToInt32(id));
            if (agreement == null)
            {
                return NotFound();
            }
            int effectedRows = await agreementService.DeleteAgreementFormAsync(agreement.AgreementId.Value);
            if (effectedRows <= 0)
            {
                return NotFound();
            }
            return Ok(agreement.AgreementId.Value);
        }
    }
}