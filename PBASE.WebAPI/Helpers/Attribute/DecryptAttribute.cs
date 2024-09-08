using PBASE.Entity.Helper;
using System;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace PBASE.WebAPI.Helpers
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class DecryptAttribute: ActionFilterAttribute
    {
        private readonly string _parameterName;

        public DecryptAttribute(string parameterName)
        {
            _parameterName = parameterName;
        }
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var value = actionContext.ActionArguments[_parameterName];
            if (value != null && value.ToString() != "0")
            {
                try
                {
                    actionContext.ActionArguments[_parameterName] = CryptoEngine.Decrypt(value.ToString());
                }
                catch (Exception)
                {
                    //DoNothing
                }
            }
        }
    }
}