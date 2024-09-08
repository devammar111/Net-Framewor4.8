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
    [MenuPermission(Menus.SystemAlerts)]
    public class SystemAlertController : BaseController
    {
        #region Initialization

        private readonly ILookupService lookupService;
        private readonly ISystemAlertService systemAlertService;

        public SystemAlertController(ILookupService lookupService, ISystemAlertService systemAlertService
        )
        {
            this.lookupService = lookupService;
            this.systemAlertService = systemAlertService;
        }

        #endregion Initialization

        // GET: api/Template/5
        [HttpGet]
        [Decrypt("id")]
        [Route("api/SystemAlertsForm/{id}")]
        public async Task<IHttpActionResult> Get([FromUri]string id)
        {
            var model = new SystemAlertViewModel();
            var vw_LookupAlertTypes = await lookupService.SelectAllvw_LookupAlertTypesAsync();
            model.vw_LookupAlertType = id == "0" ? vw_LookupAlertTypes.Where(x => x.GroupBy == "ACTIVE") : vw_LookupAlertTypes;

            if (id != "0")
            {
                SystemAlert SystemAlert = await systemAlertService.SelectBySystemAlertIdAsync(Convert.ToInt32(id));
                CopyEntityToViewModel(SystemAlert, model);
                if (SystemAlert == null)
                {
                    return NotFound();
                }
                
            }

            return Ok(model);
        }

        // POST: api/Template
        [HttpPost]
        [RequestValidation]
        [ReadOnlyValidation(Menus.SystemAlerts)]
        [Route("api/SystemAlertsForm")]
        public async Task<IHttpActionResult> Post(SystemAlertViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                SystemAlert systemAlert = new SystemAlert();
                model.IsArchived = false;
                ModelCopier.CopyModel(model, systemAlert);
               
                systemAlert.SystemAlertId = 0;
                int effectedRows = await systemAlertService.SaveSystemAlertFormAsync(systemAlert);
                if (effectedRows <= 0)
                {
                    return NotFound();
                }

                //var result = systemAlertService.SaveMultiAgreementUserType(agreement, model.AgreementUserTypeIds);
                //if (result == false)
                //{
                //    return BadRequest(systemAlertService.LastErrorMessage);
                //}

                return Ok(systemAlert.SystemAlertId);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        // PUT: api/Template/5 
        [HttpPut]
        [RequestValidation]
        [ReadOnlyValidation(Menus.SystemAlerts)]
        [Route("api/SystemAlertsForm/{id}")]
        [Decrypt("id")]
        public async Task<IHttpActionResult> Put([FromUri]string id, SystemAlertViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                SystemAlert systemAlert = await systemAlertService.SelectBySystemAlertIdAsync(Convert.ToInt32(id));
                CopyViewModelToEntity(model, systemAlert);

                int effectedRows = await systemAlertService.SaveSystemAlertFormAsync(systemAlert);
                if (effectedRows <= 0)
                {
                    return Conflict();
                }

                //var result = agreementService.SaveMultiAgreementUserType(agreement, model.AgreementUserTypeIds);
                //if (result == false)
                //{
                //    return BadRequest(agreementService.LastErrorMessage);
                //}

                return Ok("Data has been updated Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        // DELETE: api/Template/5
        [HttpDelete]
        [ReadOnlyValidation(Menus.SystemAlerts)]
        [Route("api/SystemAlertsForm/{id}")]
        [Decrypt("id")]
        public async Task<IHttpActionResult> Delete([FromUri]string id)
        {
            SystemAlert systemAlert = await systemAlertService.SelectBySystemAlertIdAsync(Convert.ToInt32(id));
            if (systemAlert == null)
            {
                return NotFound();
            }
            int effectedRows = await systemAlertService.DeleteSystemAlertFormAsync(systemAlert.SystemAlertId.Value);
            if (effectedRows <= 0)
            {
                return NotFound();
            }
            return Ok(systemAlert.SystemAlertId.Value);
        }
    }
}