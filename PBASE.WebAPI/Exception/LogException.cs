using PBASE.WebAPI.Helpers;
using System;
using System.Collections.Specialized;
using System.Text;

namespace PBASE.WebAPI
{
    public class LogException
    {
        public void Log(Exception exception, NameValueCollection serverVariables = null)
        {
            var emailService = new EmailServiceHelper();

            if (serverVariables != null)
            {
                AppendServerVariables(exception, serverVariables);
            }

            emailService.SendErrorEmail(exception);
        }

        private void AppendServerVariables(Exception exception, NameValueCollection serverVariables)
        {
            StringBuilder serverVariableKeys = new StringBuilder();

            foreach (string key in serverVariables.AllKeys)
            {
                if (!key.Equals("ALL_HTTP", StringComparison.OrdinalIgnoreCase) && !key.Equals("ALL_RAW", StringComparison.OrdinalIgnoreCase))
                {
                    serverVariableKeys.Append(key + " : ");

                    int index = 0;
                    string[] values = serverVariables.GetValues(key);
                    foreach (string value in values)
                    {
                        serverVariableKeys.AppendLine("Value " + index.ToString() + ": " + value);
                        index += 1;
                    }
                }
            }

            exception.Data.Add("ServerVariables", serverVariableKeys.ToString());
        }
    }
}
