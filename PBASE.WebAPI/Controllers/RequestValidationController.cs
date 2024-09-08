using System;
using System.Web.Helpers;
using System.Web.Http;

namespace PBASE.WebAPI.Controllers
{
    public class RequestValidationController : BaseController
    {
        [Route("api/RequestValidation")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                string token, cookieToken, formToken;
                AntiForgery.GetTokens(null, out cookieToken, out formToken);
                token = cookieToken + ":" + formToken;
                return Ok(token);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}