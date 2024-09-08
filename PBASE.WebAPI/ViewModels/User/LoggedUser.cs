using System.Collections.Generic;
using PBASE.WebAPI.ViewModels;
using PBASE.WebAPI;
using System;
using PBASE.Entity;
using static PBASE.WebAPI.Helpers.CustomValidationAttribute;

namespace PBASE.WebAPI.ViewModels
{
    public class LoggedUser
    {
        public ApplicationUser User { get; set; }
        public List<Menu> Menus { get; set; }
        public List<Menu> DashboardItems { get; set; }
        public string Database { get; set; }
        public string Version { get; set; }
        public string LastUpdated { get; set; }
        public bool? IsReadOnly { get; set; }
        public bool? IsDeleteDisabled { get; set; }
        public string ProfileImageAttachmentFileHandle { get; set; }
        public bool? IsAgree { get; set; }
        public IEnumerable<vw_SystemAlertMessages> alerts { get; set; }
    }

    public class Menu
    {
        public int? Id { get; set; }
        public int? AccessTypeId { get; set; }
    }
}