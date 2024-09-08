using System.Configuration;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using PBASE.WebAPI.Helpers;
using PBASE.WebAPI.ViewModels;

namespace PBASE.WebAPI.Controllers
{
    public class ErrorLogController : ApiController
    {
        // POST: api/ErrorLog
        [HttpPost]
        [Route("api/ErrorLog")]
        public async Task<IHttpActionResult> Post(ErrorLog_ErrorLogViewModel model)
        {
            var emailService = new EmailServiceHelper();
            var identityMessage = new IdentityMessage();
            identityMessage.Destination = ConfigurationManager.AppSettings["LogEmail"].ToString();
            identityMessage.Subject = "QCCMS-Log";
            string message = model.message;
            message += "<p>File Name: " + model.fileName  ?? "" + "</p>";
            message += "<p>Line number: " + model.lineNumber ?? "" + "</p>";
            message += "<p>TimeStamp: " + model.timestamp ?? "" + "</p>";
            identityMessage.Body = message;
            await emailService.SendAsync(identityMessage);

            return Ok();
        }
    }
}
