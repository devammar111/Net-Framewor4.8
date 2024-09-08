using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;
using PBASE.WebAPI.ViewModels;
using PBASE.Entity;
using static PBASE.WebAPI.Helpers.CustomValidationAttribute;

namespace PBASE.WebAPI.ViewModels
{
    public partial class SystemSettings_UserGroupFormViewModel : BaseViewModel
    {

        public SystemSettings_UserGroupFormViewModel()
        {
        }
        public int? UserGroupId { get; set; }
        [DisplayName("Group Name")]
        [checkValidString]
        [StringLength(50)]
        public string UserGroupName { get; set; }
        public int? AspNetRoleId { get; set; }
        public bool IsArchived { get; set; }
        public IEnumerable<int?> MenuOptionIds { get; set; }
        public IEnumerable<int?> DashboardOptionIds { get; set; }
        public IEnumerable<LookupEntity> allRoles { get; set; }
    }
}

