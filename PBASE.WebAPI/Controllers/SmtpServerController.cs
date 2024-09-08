using PBASE.Domain.Enum;
using PBASE.Helpers;
using PBASE.Service;
using Probase.GridHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using PBASE.Entity;
using PBASE.Entity.Enum;
using PBASE.WebAPI.Controllers;
using System.Web.Http;
using System.Threading.Tasks;
using System.Net.Mime;

namespace PBASE.Controllers
{
    //[FilterConfig.NoDirectAccess]
    //[Authorize]
    public class SmtpServerController : ApiController
    {

        private readonly ILookupService lookupService;
        private readonly IEmailService emailService;
        private StringBuilder errorLog = new StringBuilder();
        private List<Email> emailList = new List<Email>();

        public SmtpServerController(ILookupService lookupService, IEmailService emailService)
        {
            this.lookupService = lookupService;
            this.emailService = emailService;
        }
        
        [HttpGet]
        [AllowAnonymous]
        [Route("api/SendAllEmails")]
        public async Task<IHttpActionResult> Send()
        {
            try
            {
                var sMTPConnect = SMTPConnect.Instance;
                if (!sMTPConnect.IsSendRunning)
                {
                    ProcessLogger logger = new ProcessLogger();
                    DateTime startTime = DateTime.UtcNow;
                    sMTPConnect.IsSendRunning = true;
                    var emails = await emailService.SelectMany_EmailAsync(x => x.SentDate == null);
                    if (emails.Any())
                    {
                        try
                        {
                            SendBulkEmail(emails.ToList());
                            emailService.SaveEmailList(emails.Where(x => x.IsNotSent != true).ToList(), emailList);
                        }
                        catch (Exception ex)
                        {
                            errorLog.Append(ex.ToString() + Environment.NewLine);
                        }
                        finally
                        {
                            sMTPConnect.IsSendRunning = false;
                            var error = errorLog.ToString();
                            if (!string.IsNullOrWhiteSpace(error))
                            {
                                logger.SendProcessEmail(
                                    "Error on email process",
                                        logger.ProcessEmailBody(
                                            emails.Count(),
                                            startTime, DateTime.UtcNow,
                                            logger.dangerColor,
                                            "Exception: <br/>" + error
                                        ),
                                    true
                                );
                            }
                        }
                    }
                    sMTPConnect.IsSendRunning = false;
                }
            }
            catch (Exception ex)
            {
                var sMTPConnect = SMTPConnect.Instance;
                sMTPConnect.IsSendRunning = false;
                throw ex;
            }
            finally
            {
                var sMTPConnect = SMTPConnect.Instance;
                sMTPConnect.IsSendRunning = false;

            }
            return Ok("All emails Sent Successfully");
        }

        public async Task<IHttpActionResult> SendAll()
        {
            try
            {
                var emails = await emailService.SelectMany_EmailAsync(x => x.SentDate == null);
                foreach (var email in emails)
                {
                    ProcessEmail(email);
                }
            }
            catch (Exception ex)
            {

            }

            return Ok();
        }

        private void ProcessEmail(Email email)
        {
            string status = string.Empty;
            bool isEmailSentSuccessfully = SendEmail(email, ref status);
            var emailUpdate = emailService.SelectByEmailId(email.EmailId.Value);

            if (isEmailSentSuccessfully)
            {
                //
                // successfully sent email. 
                emailUpdate.SentDate = DateTime.UtcNow;
                emailUpdate.FormMode = FormMode.Edit;
            }

            emailUpdate.Status = EmailStatus.SentSuccessfully;

            emailService.SaveEmailForm(emailUpdate);

        }

        private bool SendBulkEmail(IEnumerable<Email> emails)
        {
            try
            {
                List<MailMessage> mailMessages = new List<MailMessage>();
                foreach (var email in emails)
                {
                    try
                    {
                        // Multiple Address of ToAddress , BCCAddress , CCAddress
                        var splitToadd = email.ToAddress != null ? (email.ToAddress.Contains(";") == true ? ";" : email.ToAddress.Contains(":") == true ? ":" : null) : null;
                        if (splitToadd != null)
                        {
                            string[] toAddressArray = email.ToAddress.Split(new string[] { splitToadd }, StringSplitOptions.RemoveEmptyEntries);
                            email.ToAddress = string.Join(",", toAddressArray);
                        }
                        var splitBccadd = email.BCCAddress != null ? (email.BCCAddress.Contains(";") == true ? ";" : email.BCCAddress.Contains(":") == true ? ":" : null) : null;
                        if (splitBccadd != null)
                        {
                            string[] bccAddressArray = email.BCCAddress.Split(new string[] { splitBccadd }, StringSplitOptions.RemoveEmptyEntries);
                            email.BCCAddress = string.Join(",", bccAddressArray);
                        }
                        var splitccadd = email.CCAddress != null ? (email.CCAddress.Contains(";") == true ? ";" : email.CCAddress.Contains(":") == true ? ":" : null) : null;
                        if (splitccadd != null)
                        {
                            string[] ccAddressArray = email.CCAddress.Split(new string[] { splitccadd }, StringSplitOptions.RemoveEmptyEntries);
                            email.CCAddress = string.Join(",", ccAddressArray);
                        }

                        MailMessage message = new MailMessage();

                        message.From = new MailAddress(email.FromAddress);
                        message.IsBodyHtml = email.IsHTML.GetValueOrDefault();
                        message.To.Add(email.ToAddress);
                        if (email.BCCAddress.IsNotNull() && email.BCCAddress.IsNotEmptyOrWhiteSpace()) message.Bcc.Add(email.BCCAddress);
                        if(email.CCAddress.IsNotNull() && email.CCAddress.IsNotEmptyOrWhiteSpace()) message.CC.Add(email.CCAddress);
                        message.Subject = email.Subject;
                        message.Body = email.Body;
                        var attachments = emailService.SelectMany_EmailAttachment(x => x.EmailId == email.EmailId).ToList();
                        foreach (var attachment in attachments)
                        {
                            var attachmentTbl = lookupService.SelectSingle_Attachment(x => x.AttachmentId == attachment.AttachmentId);
                            if(attachmentTbl.IsNotNull())
                            {
                                MemoryStream memoryStream = (MemoryStream)StreamFromUrl("https://www.filestackapi.com/api/file/" + attachmentTbl.AttachmentFileHandle + "?key=" + System.Configuration.ConfigurationManager.AppSettings["FileStackKey"] + "&signature=" + System.Configuration.ConfigurationManager.AppSettings["FileStackSignature"] + "&policy=" + System.Configuration.ConfigurationManager.AppSettings["FileStackPolicy"]);
                                message.Attachments.Add(new System.Net.Mail.Attachment(memoryStream, attachmentTbl.AttachmentFileName));
                            }
                            
                        }
                        string msgId = Guid.NewGuid().ToString();
                        message.Headers.Add("Message-Id", msgId);
                        mailMessages.Add(message);
                        emailList.Add(new Email { EmailId = email.EmailId, MessageId = msgId });
                    }
                    catch (Exception exMessage)
                    {
                        errorLog.Append("Erorr while processing emailId=" + email.EmailId + " " + exMessage.ToString() + Environment.NewLine);
                    }
                }

                SMTPBulk sMTPBulk = new SMTPBulk();
                sMTPBulk.StartEmailRun(mailMessages);
                errorLog.Append(sMTPBulk.ErrorLog);
            }
            catch (Exception ex)
            {
                errorLog.Append("SendBulkEmail failed." + ex.ToString() + Environment.NewLine);
            }
            return true;
        }

        private bool SendEmail(Email email, ref string status)
        {
            bool success = false;
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtpServer = new SmtpClient();

                message.From = new MailAddress(email.FromAddress);
                message.To.AddRangeUnique(GetAddressesList(email.ToAddress));
                message.CC.AddRangeUnique(GetAddressesList(email.CCAddress));
                message.Bcc.AddRangeUnique(GetAddressesList(email.BCCAddress));
                message.ReplyToList.AddRangeUnique(GetAddressesList(email.ReplyAddress));

                // We need to have at least one recipient.
                if (message.To.Count() > 0)
                {
                    message.Subject = email.Subject;

                    message.Body = email.Body;

                    message.IsBodyHtml = email.IsHTML.GetValueOrDefault();

                    var attachments = emailService.SelectMany_EmailAttachment(x => x.EmailId == email.EmailId).ToList();
                    foreach (var attachment in attachments)
                    {
                        var attachmentTbl = lookupService.SelectSingle_Attachment(x => x.AttachmentId == attachment.AttachmentId);
                        if (attachmentTbl.IsNotNull())
                        {
                            MemoryStream memoryStream = (MemoryStream)StreamFromUrl("https://www.filestackapi.com/api/file/" + attachmentTbl.AttachmentFileHandle + "?key=" + System.Configuration.ConfigurationManager.AppSettings["FileStackKey"] + "&signature=" + System.Configuration.ConfigurationManager.AppSettings["FileStackSignature"] + "&policy=" + System.Configuration.ConfigurationManager.AppSettings["FileStackPolicy"]);
                            message.Attachments.Add(new System.Net.Mail.Attachment(memoryStream, attachmentTbl.AttachmentFileName));
                        }
                    }
                    string msgId = Guid.NewGuid().ToString();

                    message.BodyEncoding = Encoding.UTF8;
                    message.SubjectEncoding = Encoding.UTF8;
                    ContentType mimeType = new System.Net.Mime.ContentType("text/html");
                    AlternateView htmlView = AlternateView.CreateAlternateViewFromString(message.Body, mimeType);
                    message.AlternateViews.Add(htmlView);

                    message.Headers.Add("Message-Id", "<" + msgId + "@probase.co.uk>");
                    message.Headers.Add("List-Unsubscribe", "<mailto:postal.probase.co.uk?subject=unsubscribe>");
                    smtpServer.Send(message);
                    htmlView.Dispose();

                    status = EmailStatus.SentSuccessfully;
                    success = true;
                }
            }
            catch (Exception ex)
            {
                status = ex.Message;
            }
            return success;
        }

        private MailAddressCollection GetAddressesList(string addresses)
        {
            MailAddressCollection mailAddresses = new MailAddressCollection();
            if (addresses != null)
            {
                var list = addresses.Split(new char[] { ',', ';' });

                if (list != null)
                {
                    foreach (var item in list)
                    {
                        if (item != string.Empty)
                        {
                            mailAddresses.Add(item.Trim());
                        }
                    }
                }
            }
            return mailAddresses;
        }

        private Stream StreamFromUrl(string url)
        {
            int readSize; // bytes read from response stream
            const int BUFFERSIZE = 5120;
            byte[] buffer = new byte[BUFFERSIZE]; // 5MB buffer to read from response and write to memory stream
            MemoryStream memoryStream;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            WebRequest webRequest = WebRequest.Create(url);

            using (WebResponse serverResponse = webRequest.GetResponse())
            {
                using (Stream responseStream = serverResponse.GetResponseStream())
                {
                    memoryStream = new MemoryStream();
                    while ((readSize = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                        memoryStream.Write(buffer, 0, readSize);
                }
            }

            memoryStream.Position = 0L;
            return memoryStream;
        }
    }
}
