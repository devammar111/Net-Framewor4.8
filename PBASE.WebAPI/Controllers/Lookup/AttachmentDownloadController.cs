using PBASE.Entity;
using PBASE.Service;
using System;
using System.Web.Http;
using System.Threading.Tasks;
using PBASE.WebAPI.Helpers;
using PBASE.WebAPI.ViewModels;

namespace PBASE.WebAPI.Controllers
{
    public class AttachmentDownloadController : BaseController
    {
        #region Initialization

        private readonly ILookupService lookupService;

        public AttachmentDownloadController(ILookupService lookupService
        )
        {
            this.lookupService = lookupService;
        }

        #endregion Initialization

        [AllowAnonymous]
        [Decrypt("id")]
        public async Task<IHttpActionResult> Get([FromUri]string id)
        {
            try
            {
                int attachmentid = IntParseId(id);
                Attachment attachment = await lookupService.SelectSingle_AttachmentAsync(x => x.AttachmentId == attachmentid);
                if (attachment != null)
                {
                    byte[] fileContents = ByteArrayFromFileHandle(attachment.AttachmentFileHandle);
                    return Ok(new FileViewModel(fileContents, attachment.AttachmentFileName, attachment.AttachmentFileType));
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("api/GetAttachmentFileHandler/{id}")]
        [Decrypt("id")]
        public async Task<IHttpActionResult> GetAttachmentFileHandler([FromUri] string id)
        {
            try
            {
                var attachmentid = Convert.ToInt32(id);
                Attachment attachment = await lookupService.SelectSingle_AttachmentAsync(x => x.AttachmentId == attachmentid);
                if (attachment != null)
                {
                    return Ok(attachment.AttachmentFileHandle);
                }
                return Ok("Attachment Not Found!!");
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

    }
}
