using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Threading;
using System.Text;

namespace PBASE.Helpers
{
    public class SMTPBulk
    {
        private const int _clientcount = 15;
        private SmtpClient[] _smtpClients = new SmtpClient[_clientcount + 1];
        private CancellationTokenSource _cancelToken;
        private StringBuilder errorLog = new StringBuilder();

        public SMTPBulk()
        {
            setupSMTPClients();
        }

        public string ErrorLog { get { return errorLog.ToString(); } }

        public void StartEmailRun(List<MailMessage> data)
        {
            try
            {
                ParallelOptions parallelOptions = new ParallelOptions();
                //Create a cancellation token so you can cancel the task.
                _cancelToken = new CancellationTokenSource();
                parallelOptions.CancellationToken = _cancelToken.Token;
                //Manage the MaxDegreeOfParallelism instead of .NET Managing this. We dont need 500 threads spawning for this.
                parallelOptions.MaxDegreeOfParallelism = System.Environment.ProcessorCount * 2;
                try
                {
                    Parallel.ForEach(data, parallelOptions, (MailMessage mailMessage) =>
                    {
                        try
                        {
                            mailMessage.Priority = MailPriority.Normal;
                            SendEmail(mailMessage);
                        }
                        catch (Exception ex)
                        {
                            errorLog.Append("StartEmailRun failed." + ex.ToString() + Environment.NewLine);
                        }
                    });
                }
                catch (OperationCanceledException)
                {
                    //User has cancelled this request.
                }
            }
            finally
            {
                disposeSMTPClients();
            }
        }

        public void CancelEmailRun()
        {
            _cancelToken.Cancel();
        }

        private void SendEmail(MailMessage msg)
        {
            try
            {
                bool _gotlocked = false;
                while (!_gotlocked)
                {
                    //Keep looping through all smtp client connections until one becomes available
                    for (int i = 0; i <= _clientcount; i++)
                    {
                        if (System.Threading.Monitor.TryEnter(_smtpClients[i]))
                        {
                            try
                            {
                                _smtpClients[i].Send(msg);
                            }
                            finally
                            {
                                System.Threading.Monitor.Exit(_smtpClients[i]);
                            }
                            _gotlocked = true;
                            break; // TODO: might not be correct. Was : Exit For
                        }
                    }
                    //Do this to make sure CPU doesn't ramp up to 100%
                    System.Threading.Thread.Sleep(1);
                }
            }
            finally
            {
                msg.Dispose();
            }
        }

        private void setupSMTPClients()
        {
            for (int i = 0; i <= _clientcount; i++)
            {
                try
                {
                    SmtpClient _client = new SmtpClient();
                    _smtpClients[i] = _client;
                }
                catch (Exception ex)
                {
                    errorLog.Append("setupSMTPClients failed." + ex.ToString() + Environment.NewLine);
                }
            }
        }

        private void disposeSMTPClients()
        {
            for (int i = 0; i <= _clientcount; i++)
            {
                try
                {
                    _smtpClients[i].Dispose();
                }
                catch (Exception ex)
                {
                    errorLog.Append("disposeSMTPClients failed." + ex.ToString() + Environment.NewLine);
                }
            }
        }
    }
}