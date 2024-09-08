using PBASE.Entity;
using PBASE.Entity.Enum;
using PBASE.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace PBASE.WebAPI.Helpers
{
    public static class UserLogHelper
    {
        private static readonly IUserLogService userLogService = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IUserLogService)) as IUserLogService;
        public static void LogAccountActivity(int? accountKey, string requestType, bool? IsStatus, string ipAddress, string location)
        {
            AspNetUserLogs userAccountLog = new AspNetUserLogs();
            userAccountLog.FormMode = FormMode.Create;
            userAccountLog.IsStatus = IsStatus;
            userAccountLog.AspNetUserKey = accountKey;
            userAccountLog.RequestType = requestType;
            userAccountLog.IPAddress = ipAddress;
            userAccountLog.Location = location;
            userLogService.SaveAspNetUserLogsForm(userAccountLog);
        }
    }
}