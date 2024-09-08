using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Net.Mime;

namespace PBASE.Helpers
{
    public class ProcessLogger
    {
        private Dictionary<string, Stream> attachments = new Dictionary<string, Stream>();
        private readonly string logsEmailAddress = System.Configuration.ConfigurationManager.AppSettings["LogsEmailAddress"] as string;
        public readonly string successColor = "#71bc37";
        public readonly string dangerColor = "red";
        public readonly string attachmentText = "For record details see attached excel files.";

        public string ProcessEmailBody(int updatedRecordsCount, DateTime startTime, DateTime endTime, string statusColor, string errorMessage = "")
        {
            string body = string.Empty;
            var elpasedTime = endTime - startTime;
            using (StreamReader reader = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/Content/templates/DbSyncEmailTemplate.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{{StatusColor}}", statusColor);
            body = body.Replace("{{StartTime}}", startTime.ToString("HH:mm"));
            body = body.Replace("{{EndTime}}", endTime.ToString("HH:mm"));
            body = body.Replace("{{ElapsedTime}}", string.Format("{0}:{1}:{2}", elpasedTime.Hours, elpasedTime.Minutes, elpasedTime.Seconds));
            body = body.Replace("{{AddedCount}}", updatedRecordsCount.ToString());
            body = body.Replace("{{ModifiedCount}}", "0");
            body = body.Replace("{{ErrorMessage}}", errorMessage);
            body = statusColor == successColor ? body.Replace("{{Heading}}", "Success") : body.Replace("{{Heading}}", "Error");
            body = attachments.Any() ? body.Replace("{{AttachmentText}}", attachmentText) : body.Replace("{{AttachmentText}}", "");
            return body;
        }

        public void SendProcessEmail(string subject, string body, bool isHtml)
        {
            SmtpClient smtpServer = new SmtpClient();
            MailMessage message = new MailMessage();
            message.To.Add(logsEmailAddress.Trim());
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = isHtml;

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
        }
    }
}