using Microsoft.AspNet.Identity;
using System;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mime;

namespace PBASE.WebAPI.Helpers
{
    public class EmailServiceHelper : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            await configSendGridasync(message);
        }

        public void SendErrorEmail(Exception ex)
        {
            MailMessage mailMessage = new MailMessage();
            SmtpClient smtpServer = new SmtpClient();
            mailMessage.To.Add(System.Configuration.ConfigurationManager.AppSettings["LogEmail"]);
            mailMessage.Subject = "QCCMS-Log:" + ex.Message;

            var body = new StringBuilder(ex.ToString());

            foreach (var item in ex.Data.Values)
            {
                body.Append("ServerVariables:" + item);
            }
            
            mailMessage.Body = body.ToString();
            mailMessage.BodyEncoding = Encoding.UTF8;
            mailMessage.SubjectEncoding = Encoding.UTF8;
            ContentType mimeType = new System.Net.Mime.ContentType("text/html");
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mailMessage.Body, mimeType);
            mailMessage.AlternateViews.Add(htmlView);

            string msgId = Guid.NewGuid().ToString();
            mailMessage.Headers.Add("Message-Id", "<" + msgId + "@probase.co.uk>");
            mailMessage.Headers.Add("List-Unsubscribe", "<mailto:postal.probase.co.uk?subject=unsubscribe>");

            smtpServer.Send(mailMessage);
            htmlView.Dispose();
        }

        private async Task configSendGridasync(IdentityMessage message)
        {
            MailMessage mailMessage = new MailMessage();
            SmtpClient smtpServer = new SmtpClient();
            mailMessage.To.Add(message.Destination);
            mailMessage.Subject = message.Subject;
            mailMessage.Body = message.Body;
            mailMessage.IsBodyHtml = true;

            mailMessage.BodyEncoding = Encoding.UTF8;
            mailMessage.SubjectEncoding = Encoding.UTF8;
            ContentType mimeType = new System.Net.Mime.ContentType("text/html");
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mailMessage.Body, mimeType);
            mailMessage.AlternateViews.Add(htmlView);

            string msgId = Guid.NewGuid().ToString();
            mailMessage.Headers.Add("Message-Id", "<" + msgId + "@probase.co.uk>");
            mailMessage.Headers.Add("List-Unsubscribe", "<mailto:postal.probase.co.uk?subject=unsubscribe>");

            await smtpServer.SendMailAsync(mailMessage);
            htmlView.Dispose();
        }
    }
}