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
    [MenuPermission(Menus.Templates)]
    public class TemplateController : BaseController
    {
        #region Initialization

        private readonly ILookupService lookupService;

        public TemplateController(ILookupService lookupService
        )
        {
            this.lookupService = lookupService;
        }

        #endregion Initialization

        // GET: api/Template/5
        [HttpGet]
        [Decrypt("id")]
        [Route("api/Template/{id}")]
        public async Task<IHttpActionResult> Get([FromUri]string id)
        {
            var model = new TemplateViewModel();
            var Template_Types = await lookupService.SelectAllvw_LookupTemplateTypesAsync();
            model.vw_LookupTemplateTypes = id == "0" ? Template_Types.Where(x => x.GroupBy == "ACTIVE") : Template_Types;

            var TemplateTags = await lookupService.SelectAllTemplateTagsAsync();
            model.TemplateTags = TemplateTags.Select(x => new LookupEntity() { LookupId = x.TemplateTagId, LookupValue = x.Tag, LookupExtraInt = x.TemplateTypeId, LookupExtraText = x.TagDescription }).OrderBy(x => x.LookupValue).ToList();

            if (id != "0")
            {
                Template Template = await lookupService.SelectByTemplateIdAsync(Convert.ToInt32(id));
                CopyEntityToViewModel(Template, model);
                if (Template == null)
                {
                    return NotFound();
                }

                if (Template.TemplateTypeId != null)
                {
                    model.TemplateTagIds = lookupService.SelectMany_TemplateTag(x => x.TemplateTypeId == Template.TemplateTypeId).Select(x => x.TemplateTagId.Value).ToList();
                }
                

                Attachment Attachment = await lookupService.SelectByAttachmentIdAsync(Template.AttachmentId.Value);
                CopyEntityToViewModel(Attachment, model);
            }

            return Ok(model);
        }

        // POST: api/Template
        [HttpPost]
        [RequestValidation]
        [ReadOnlyValidation(Menus.Templates)]
        [Route("api/Template")]
        public async Task<IHttpActionResult> Post(TemplateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Template template = new Template();
            base.CopyViewModelToEntity(model, template);
            template.IsArchived = false;
            template.TemplateId = 0;
            template.AttachmentId = base.ProcessAttchment(lookupService, new Attachment
            {
                AttachmentId = template.AttachmentId.HasValue ? template.AttachmentId.Value : 0,
                AttachmentFileName = model.AttachmentFileName,
                AttachmentFileSize = model.AttachmentFileSize,
                AttachmentFileType = model.AttachmentFileType,
                AttachmentFileHandle = model.AttachmentFileHandle,
                ConnectedTable = "Template",
                ConnectedField = "AttachmentId",
                IsArchived = false
            }
                );
            int effectedRows = await lookupService.SaveTemplateFormAsync(template);
            if (effectedRows <= 0)
            {
                return NotFound();
            }
            return Ok(template.TemplateId);
        }

        // PUT: api/Template/5 
        [HttpPut]
        [Decrypt("id")]
        [RequestValidation]
        [ReadOnlyValidation(Menus.Templates)]
        [Route("api/Template/{id}")]
        public async Task<IHttpActionResult> Put([FromUri]string id, TemplateViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var updatedModel = new TemplateViewModel();
                Template template = await lookupService.SelectByTemplateIdAsync(Convert.ToInt32(id));
                CopyViewModelToEntity(model, template);
                template.AttachmentId = ProcessAttchment
                    (
                        lookupService,
                        new Attachment
                        {
                            AttachmentId = template.AttachmentId.HasValue ? template.AttachmentId.Value : 0,
                            AttachmentFileName = model.AttachmentFileName,
                            AttachmentFileSize = model.AttachmentFileSize,
                            AttachmentFileType = model.AttachmentFileType,
                            AttachmentFileHandle = model.AttachmentFileHandle,
                            IsArchived = false
                        }
                    );
                int effectedRows = await lookupService.SaveTemplateFormAsync(template);
                if (effectedRows <= 0)
                {
                    return NotFound();
                }
                CopyEntityToViewModel(template, updatedModel);
                return Ok(updatedModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        // DELETE: api/Template/5
        [HttpDelete]
        [Decrypt("id")]
        [ReadOnlyValidation(Menus.Templates)]
        [Route("api/Template/{id}")]
        public async Task<IHttpActionResult> Delete([FromUri]string id)
        {
            Template template = await lookupService.SelectByTemplateIdAsync(Convert.ToInt32(id));
            if (template == null)
            {
                return NotFound();
            }
            int effectedRows = await lookupService.DeleteTemplateFormAsync(template.TemplateId.Value);
            if (effectedRows <= 0)
            {
                return NotFound();
            }
            return Ok(template);
        }

        [HttpGet]
        [Route("api/checkTemplate/{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var model = new TemplateViewModel();
            Template Template = await lookupService.SelectSingle_TemplateAsync(x => x.TemplateTypeId == id);
            vw_LookupTemplateType vw_LookupTemplateType = lookupService.SelectSingle_vw_LookupTemplateType(x => x.TemplateTypeId == id);
            vw_LookupTemplateAllowedType vw_LookupTemplateAllowedType = lookupService.SelectSingle_vw_LookupTemplateAllowedType(x => x.TemplateAllowedTypeId == vw_LookupTemplateType.LookupExtraInt);
            if (Template != null && vw_LookupTemplateAllowedType.TemplateAllowedType == "Single")
            {
                return Ok("yes");
            }

            return Ok("no");
        }
    }
}