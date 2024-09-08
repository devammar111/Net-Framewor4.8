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
    public partial class SystemSettings_UserMenuOptionRoleViewModel : BaseViewModel
    {

        public SystemSettings_UserMenuOptionRoleViewModel()
        {
        }
        public int? UserMenuOptionRoleId { get; set; }
        public int? MenuOptionId { get; set; }
        public int? AspNetRoleId { get; set; }
        public IEnumerable<int> AspNetRoleIds { get; set; }
        public bool IsArchived { get; set; }
        public IEnumerable<LookupEntity> allRoles { get; set; }
    }
}

