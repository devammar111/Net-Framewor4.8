using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace PBASE.WebAPI
{
    public class InternalExceptionLogger : ExceptionLogger
    {
        private const string HttpContextBaseKey = "MS_HttpContext";

        public override void Log(ExceptionLoggerContext context)
        {
            HttpContext httpContext = GetHttpContext(context.Request); // Retrieve the current HttpContext instance for this request.
            NameValueCollection serverVariables = null;

            LogException logException = new LogException();

            try
            {
                if (httpContext != null && httpContext.Request != null
                    && httpContext.Request.ServerVariables != null)
                {
                    serverVariables = httpContext.Request.ServerVariables;
                }

                logException.Log(context.Exception, serverVariables);
            }
            catch (Exception e)
            {
                SwallowException(e);
            }
        }

        private static HttpContext GetHttpContext(HttpRequestMessage request)
        {
            HttpContextBase contextBase = GetHttpContextBase(request);

            if (contextBase == null)
            {
                return null;
            }

            return ToHttpContext(contextBase);
        }

        private static HttpContextBase GetHttpContextBase(HttpRequestMessage request)
        {
            object value;

            if (request == null || !request.Properties.TryGetValue(HttpContextBaseKey, out value))
            {
                return null;
            }

            return value as HttpContextBase;
        }

        private static HttpContext ToHttpContext(HttpContextBase contextBase)
        {
            return contextBase.ApplicationInstance.Context;
        }

        private static void SwallowException(Exception ex)
        {
            ;
        }
    }
}
