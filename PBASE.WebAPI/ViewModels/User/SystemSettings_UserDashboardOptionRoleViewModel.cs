using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;
using PBASE.WebAPI.ViewModels;
using PBASE.Entity;

namespace PBASE.WebAPI.ViewModels
{
    public partial class SystemSettings_UserDashboardOptionRoleViewModel : BaseViewModel
    {

        public SystemSettings_UserDashboardOptionRoleViewModel()
        {
        }

        public int? UserDashboardOptionRoleId { get; set; }
        
        public int? DashboardOptionId { get; set; }
        public int? AspNetRoleId { get; set; }
        public IEnumerable<int> AspNetRoleIds { get; set; }
        public bool IsArchived { get; set; }
        public IEnumerable<LookupEntity> allRoles { get; set; }
    }
}

