using System;
using System.Linq;
using System.Web.Http;
using System.Threading.Tasks;
using PBASE.Service;
using PBASE.WebAPI.ViewModels;
using PBASE.WebAPI.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Net.Http;
using Microsoft.AspNet.Identity.Owin;
using PBASE.Entity;
using PBASE.Entity.Helper;

namespace PBASE.WebAPI.Controllers
{
    [MenuPermission(Menus.SystemAdministration)]
    [RoutePrefix("api")]
    public class ApplicationInformationController : BaseController
    {
        #region Initialization

        private readonly IEmailService emailService;
        private readonly ILookupService lookupService;

        public ApplicationInformationController(IEmailService emailService, ILookupService lookupService)
        {
            this.emailService = emailService;
            this.lookupService = lookupService;
        }

        #endregion Initialization

        #region ApplicationInformation

        // GET: api/ApplicationInformation/5
        [HttpGet]
        [Route("ApplicationInformation")]
        public async Task<IHttpActionResult> Get()
        {
            ApplicationInformationFormViewModel model = new ApplicationInformationFormViewModel();

            var app_info = await emailService.SelectAllApplicationInformationsAsync();
            ApplicationInformation ApplicationInformation = app_info.ToList()[0];
            if (ApplicationInformation == null)
            {
                return NotFound();
            }

            ModelCopier.CopyModel(ApplicationInformation, model);
            model.EncApplicationInformationId = CryptoEngine.Encrypt(ApplicationInformation.ApplicationInformationId.ToString());

            return Ok(model);
        }

        // PUT: api/ApplicationInformation/5
        [HttpPut]
        [RequestValidation]
        [ReadOnlyValidation(Menus.SystemAdministration)]
        [Decrypt("id")]
        [Route("ApplicationInformation/{id}")]
        public async Task<IHttpActionResult> Put([FromUri] string id, ApplicationInformationFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ApplicationInformation ApplicationInformation = await emailService.SelectByApplicationInformationIdAsync(Convert.ToInt32(id));

            base.CopyViewModelToEntity(model, ApplicationInformation);
            int effectedRows = await emailService.SaveApplicationInformationFormAsync(ApplicationInformation);
            if (effectedRows <= 0)
            {
                return NotFound();
            }
            return Ok(ApplicationInformation);

        }

        // DELETE: api/ApplicationInformation/5
        [HttpDelete]
        [Decrypt("id")]
        [ReadOnlyValidation(Menus.SystemAdministration)]
        [Route("ApplicationInformation/{id}")]
        public async Task<IHttpActionResult> Delete([FromUri] string id)
        {
            ApplicationInformation ApplicationInformation = await emailService.SelectByApplicationInformationIdAsync(Convert.ToInt32(id));
            if (ApplicationInformation == null)
            {
                return NotFound();
            }

            int effectedRows = await emailService.DeleteApplicationInformationFormAsync(Convert.ToInt32(id));
            if (effectedRows <= 0)
            {
                return NotFound();
            }
            return Ok(ApplicationInformation);
        }

        #endregion

        [HttpGet]
        [Route("checkApplicationInformation/{id}")]
        public async Task<IHttpActionResult> GetApplicationInformation(int id)
        {
            var model = new TemplateViewModel();
            ApplicationInformation ApplicationInformation = await emailService.SelectSingle_ApplicationInformationAsync(x => x.ApplicationInformationId == id);
            if (ApplicationInformation != null)
            {
                return Ok("yes");
            }

            return Ok("no");
        }
    }
}