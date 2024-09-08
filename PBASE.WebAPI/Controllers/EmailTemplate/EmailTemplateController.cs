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

namespace PBASE.WebAPI.Controllers
{
    [MenuPermission(Menus.EmailTemplate)]
    [RoutePrefix("api")]
    public class EmailTemplateController : BaseController
    {
        #region Initialization

        private readonly IEmailTemplateService emailtemplateService;
        private readonly ILookupService lookupService;

        public EmailTemplateController(IEmailTemplateService emailtemplateService, ILookupService lookupService)        
        {
            this.emailtemplateService = emailtemplateService;
            this.lookupService = lookupService;
        }

        #endregion Initialization

        #region EmailTemplate

        // GET: api/EmailTemplate/5
        [HttpGet]
        [Decrypt("id")]
        [Route("EmailTemplate/{id}")]
        public async Task<IHttpActionResult> Get([FromUri]string id)
        {
            EmailTemplateFormViewModel model = new EmailTemplateFormViewModel();

            var Template_Types = await lookupService.SelectAllvw_LookupEmailTemplateTypesAsync();
            model.vw_LookupEmailTemplateType = id == "0" ? Template_Types.Where(x => x.GroupBy == "ACTIVE") : Template_Types;

            var EmailTypes = await lookupService.SelectAllvw_LookupEmailTypesAsync();
            model.vw_LookupEmailType = id == "0" ? EmailTypes.Where(x => x.GroupBy == "ACTIVE") : EmailTypes;

            var vw_LookupFromEmailAddresses = await lookupService.SelectAllvw_LookupFromEmailAddresssAsync();
            model.vw_LookupFromEmailAddress = id == "0" ? vw_LookupFromEmailAddresses.Where(x => x.GroupBy == "ACTIVE") : vw_LookupFromEmailAddresses;

            var EmailTemplateTags = await emailtemplateService.SelectAllEmailTemplateTagsAsync();
            model.EmailTemplateTags = EmailTemplateTags.Select(x => new LookupEntity() { LookupId = x.EmailTemplateTagId, LookupValue = x.Tag, LookupExtraInt = x.EmailTemplateTypeId, LookupExtraText = x.TagDescription }).OrderBy(x => x.LookupValue).ToList();

            if (id != "0")
            {
                EmailTemplate EmailTemplate = await emailtemplateService.SelectByEmailTemplateIdAsync(Convert.ToInt32(id));
                if (EmailTemplate == null)
                {
                    return NotFound();
                }

                if (EmailTemplate.EmailTemplateTypeId != null)
                {
                    model.EmailTemplateTagIds = emailtemplateService.SelectMany_EmailTemplateTag(x => x.EmailTemplateTypeId == EmailTemplate.EmailTemplateTypeId).Select(x => x.EmailTemplateTagId.Value).ToList();
                }

                ModelCopier.CopyModel(EmailTemplate, model);     
            }

            return Ok(model);
        }

        // POST: api/EmailTemplate
        [HttpPost]
        [RequestValidation]
        [ReadOnlyValidation(Menus.EmailTemplate)]
        [Route("EmailTemplate")]
        public async Task<IHttpActionResult> Post(EmailTemplateFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EmailTemplate EmailTemplate = new EmailTemplate();
            base.CopyViewModelToEntity(model, EmailTemplate);
            int effectedRows = await emailtemplateService.SaveEmailTemplateFormAsync(EmailTemplate);
            if (effectedRows <= 0)
            {
                return NotFound();
            }

            //var result = emailtemplateService.SaveEmailTemplateTagMultiple(EmailTemplate, model.EmailTemplateTagIds);
            //if (result == false)
            //{
            //    return BadRequest(emailtemplateService.LastErrorMessage);
            //}

            return Ok(EmailTemplate);

        }

        // PUT: api/EmailTemplate/5
        [HttpPut]
        [RequestValidation]
        [ReadOnlyValidation(Menus.EmailTemplate)]
        [Decrypt("id")]
        [Route("EmailTemplate/{id}")]
        public async Task<IHttpActionResult> Put([FromUri]string id, EmailTemplateFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            EmailTemplate EmailTemplate = await emailtemplateService.SelectByEmailTemplateIdAsync(Convert.ToInt32(id));

            base.CopyViewModelToEntity(model, EmailTemplate);
            int effectedRows = await emailtemplateService.SaveEmailTemplateFormAsync(EmailTemplate);
            if (effectedRows <= 0)
            {
                return NotFound();
            }
            return Ok(EmailTemplate);

        }

        // DELETE: api/EmailTemplate/5
        [HttpDelete]
        [Decrypt("id")]
        [ReadOnlyValidation(Menus.EmailTemplate)]
        [Route("EmailTemplate/{id}")]
        public async Task<IHttpActionResult> Delete([FromUri]string id)
        {
            EmailTemplate EmailTemplate = await emailtemplateService.SelectByEmailTemplateIdAsync(Convert.ToInt32(id));
            if (EmailTemplate == null)
            {
                return NotFound();
            }

            int effectedRows = await emailtemplateService.DeleteEmailTemplateFormAsync(Convert.ToInt32(id));
            if (effectedRows <= 0)
            {
                return NotFound();
            }
            return Ok(EmailTemplate);
        }

        #endregion

        [HttpGet]
        [Route("checkEmailTemplate/{id}")]
        public async Task<IHttpActionResult> GetEmailTemplate(int id)
        {
            var model = new TemplateViewModel();
            EmailTemplate EmailTemplate = await emailtemplateService.SelectSingle_EmailTemplateAsync(x => x.EmailTemplateTypeId == id);
            if (EmailTemplate != null)
            {
                return Ok("yes");
            }

            return Ok("no");
        }
    }
}
