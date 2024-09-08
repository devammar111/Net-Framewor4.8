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
    public partial class UserGroupDashboardOption_UserGroupDashboardOptionFormViewModel : BaseViewModel
    {

        public UserGroupDashboardOption_UserGroupDashboardOptionFormViewModel()
        {
        }

        public int? UserGroupDashboardOptionId { get; set; }
        public int? UserGroupId { get; set; }
        public int? DashboardOptionId { get; set; }
        public bool IsArchived { get; set; }
    }
}

