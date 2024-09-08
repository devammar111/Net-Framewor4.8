using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Helpers;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace PBASE.WebAPI.Helpers
{
    public class RequestValidationAttribute : ActionFilterAttribute
    {
        private const string RequestValidationHeader = "__RequestValidation";
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            string cookieToken, formToken;
            HttpRequestHeaders headers = actionContext.Request.Headers;
            IEnumerable<string> requestValidationToken;
            if (!headers.TryGetValues(RequestValidationHeader, out requestValidationToken))
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadGateway);
                return;
            }
            string[] tokens = requestValidationToken.First().Split(':');
            if (tokens.Length == 2)
            {
                cookieToken = tokens[0].Trim();
                formToken = tokens[1].Trim();
            }
            else
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadGateway);
                return;
            }
            try
            {
                AntiForgery.Validate(cookieToken, formToken);
            }
            catch (Exception ex)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadGateway);
            }
        }
    }
}