using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using PBASE.WebAPI.ViewModels;
using PBASE.Entity.Enum;
using PBASE.WebAPI.Helpers;
using System.Web;
using System.Net;
using PBASE.Entity.Helper;
using System.IO;
using PBASE.Entity;
using PBASE.Service;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Net.Mime;

namespace PBASE.WebAPI.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;
        private static readonly HttpClient client = new HttpClient();

        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            try
            {
                int loginLimit = 0;
                var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();
                var systemAlertService = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(ISystemAlertService)) as ISystemAlertService;
                var emailService = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IEmailService)) as IEmailService;
                var lookupService = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(ILookupService)) as ILookupService;
                var userService = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IUserService)) as IUserService;
                var applicationInformation = emailService.SelectAllApplicationInformations().FirstOrDefault();
                if (applicationInformation != null && applicationInformation.LoginLimit.HasValue)
                {
                    loginLimit = applicationInformation.LoginLimit.Value;
                }
                String clientIP = GetIPAddress();
                string location = await GetLocationByIpAsync(clientIP);

                try
                {
                    var emailUser = await userManager.FindByEmailAsync(context.UserName);
                    var invalidEmailLog = await userService.SelectSingle_AspNetUsersInvalidAsync(x => x.Email == context.UserName);
                    if (emailUser != null)
                    {
                        //Case 1 For Login Limit
                        if (emailUser.AccessFailedCount >= loginLimit)
                        {
                            context.SetError("invalid_grant", "Access is denied, please contact an administrator.");
                            return;
                        }
                        var user = await userManager.FindAsync(context.UserName, context.Password);
                        //var user =  await userManager.FindByEmailAsync(context.UserName);
                        if (user == null)
                        {
                            //Case 2 For Login Limit
                            emailUser.AccessFailedCount++;
                            await userManager.UpdateAsync(emailUser);
                            UserLogHelper.LogAccountActivity(emailUser.Id, Enum.GetName(typeof(RequestType), 0), false, clientIP, location);
                            context.SetError("invalid_grant", "The user name and/or password is incorrect.");
                            return;
                        }
                        if (user.LockoutEnabled)
                        {
                            UserLogHelper.LogAccountActivity(emailUser.Id, Enum.GetName(typeof(RequestType), 0), false, clientIP, location);
                            context.SetError("invalid_grant", "The user is locked.");
                            return;
                        }
                        if (context.Scope[0] == "")
                        {
                            UserLogHelper.LogAccountActivity(emailUser.Id, Enum.GetName(typeof(RequestType), 0), false, clientIP, location);
                            context.SetError("invalid_grant", "Could not get user's IP.");
                            return;
                        }
                        string CurrentUsersIP = context.Scope[0];

                        var SafeIP = await lookupService.SelectSingle_SafeIPsAsync(x => x.IPAddress == CurrentUsersIP);
                        if (SafeIP == null)
                        {
                            var message =  TwoFactorVerfication(emailUser.Email);
                            context.SetError("verification_code", message);
                            return;
                        }

                        //Case 3 For Login Limit
                        user.AccessFailedCount = 0;
                        await userManager.UpdateAsync(user);
                        var closeAlerts = await systemAlertService.SelectSingle_vw_SystemAlertIsClosedAsync(x => x.CloseText != null && x.CloseText != string.Empty);
                        UserLogHelper.LogAccountActivity(user.Id, Enum.GetName(typeof(RequestType), 0), true, clientIP, location);
                        ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager,
                           OAuthDefaults.AuthenticationType);
                        ClaimsIdentity cookiesIdentity = await user.GenerateUserIdentityAsync(userManager,
                            CookieAuthenticationDefaults.AuthenticationType);

                        AuthenticationProperties properties = CreateProperties(user.UserName, user.Id, closeAlerts);
                        AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
                        context.Validated(ticket);
                        context.Request.Context.Authentication.SignIn(cookiesIdentity);
                    }
                    else if (invalidEmailLog == null)
                    {
                        //Case 4 For Login Limit
                        invalidEmailLog = new AspNetUsersInvalid();
                        invalidEmailLog.Id = 0;
                        invalidEmailLog.Email = context.UserName;
                        invalidEmailLog.AccessFailedCount = 1;
                        invalidEmailLog.LastAccessDate = DateTime.Now;
                        invalidEmailLog.IPAddress = clientIP;
                        await userService.SaveAspNetUsersInvalidFormAsync(invalidEmailLog);
                        context.SetError("invalid_grant", "The user name and/or password is incorrect.");
                        return;
                    }
                    else
                    {
                        //Case 5 For Login Limit
                        invalidEmailLog.AccessFailedCount++;
                        invalidEmailLog.LastAccessDate = DateTime.Now;
                        invalidEmailLog.IPAddress = clientIP;
                        await userService.SaveAspNetUsersInvalidFormAsync(invalidEmailLog);
                        context.SetError("invalid_grant", "Access is denied.");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    context.SetError(ex.Message);
                    return;
                }

            }
            catch (Exception ex)
            {
                // Do nothing
            }
        }

        protected string TwoFactorVerfication(string toAddress)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtpServer = new SmtpClient();

                message.From = new MailAddress("admin@probase.co.uk");
                message.To.Add(toAddress);

                // We need to have at least one recipient.
                if (message.To.Count() > 0)
                {
                    message.Subject = "OTP";
                    message.Body = new Random().Next(1000, 9999).ToString();
                    message.IsBodyHtml = false;
                    smtpServer.Send(message);
                }
                return "OTP sent to your Email.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        protected string GetIPAddress()
        {
            HttpRequest currentRequest = HttpContext.Current.Request;
            string ipList = currentRequest.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipList))
            {
                return ipList.Split(',')[0];
            }

            return currentRequest.ServerVariables["REMOTE_ADDR"];
        }

        public static async Task<string> GetLocationByIpAsync(string IP)
        {
            try
            {
                var ipParts = IP.Split(':');
                if (ipParts[0] == "")
                {
                    return "";
                }
                string loction = "";
                var values = new Dictionary<string, string>
                {
                    { "ip", ipParts[0] }
                };

                var content = new FormUrlEncodedContent(values);

                var response = await client.PostAsync("https://iplocation.com", content);

                var responseString = await response.Content.ReadAsStringAsync();
                var obj = JObject.Parse(responseString);
                var City = (string)obj["city"];
                var Country = (string)obj["country_name"];
                if (City != null || City != "")
                {
                    loction = City;
                }
                if (Country != null || Country != "")
                {
                    if (loction != "")
                    {
                        loction = loction + ", " + Country;
                    }
                    else
                    {
                        loction = Country;
                    }
                }
                return loction;
            }
            catch (Exception ex)
            {
                //No need to catch
            }

            return string.Empty;
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string userName, int Id, vw_SystemAlertIsClosed closeAlertMessage)
        {
            try
            {
                IDictionary<string, string> data = new Dictionary<string, string>{
                { "userId", CryptoEngine.Encrypt(Id.ToString()) },
                { "userName", userName }
                };
                if (closeAlertMessage != null)
                {
                    data.Add("closeAlertMessage", closeAlertMessage.CloseText);
                }
                return new AuthenticationProperties(data);
            }
            catch (Exception ex)
            { 
                //Do nothing
            }
            return null;
        }
        
        public static AuthenticationProperties CreateTwoFactorProperties(string OTP)
        {
            try
            {
                IDictionary<string, string> data = new Dictionary<string, string>
                {                
                    { "OTP", OTP }
                };              
                return new AuthenticationProperties(data);
            }
            catch (Exception ex)
            { 
                //Do nothing
            }
            return null;
        }
    }
}