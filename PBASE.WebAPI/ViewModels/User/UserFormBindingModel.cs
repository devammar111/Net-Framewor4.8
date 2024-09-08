using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using PBASE.Entity.Enum;
using PBASE.Entity;
using System.ComponentModel;
using static PBASE.WebAPI.Helpers.CustomValidationAttribute;

namespace PBASE.WebAPI.ViewModels
{
    public class UserFormBindingModel : BaseViewModel
    {
        public UserFormBindingModel()
        {

        }
        public int Id { get; set; }
        public int LoggedInUserId { get; set; }

        [DisplayName("Username")]
        [checkValidString]
        [StringLength(256)]
        public string UserName { get; set; }

        [DisplayName("First Name")]
        [checkValidString]
        [StringLength(50)]
        public string FirstName { get; set; }

        [DisplayName("LastName")]
        [checkValidString]
        [StringLength(50)]
        public string LastName { get; set; }

        public string[] AssignedRoles { get; set; }

        [DisplayName("Email Address")]
        [StringLength(256)]
        public string Email { get; set; }
        [DisplayName("User Type")]
        public int? UserTypeId { get; set; }
        [checkValidString]
        public string Password { get; set; }
        [checkValidString]
        public string ConfirmPassword { get; set; }
        public bool LockoutEnabled { get; set; }
        public int? UserAccessTypeId { get; set; }
        public IEnumerable<int?> DashboardOptionIds { get; set; }
        public IEnumerable<int?> MenuOptionIds { get; set; }
        public int? UserGroupId { get; set; }
        public IEnumerable<LookupEntity> vw_lookuprole { get; set; }
        public IEnumerable<LookupEntity> vw_lookupuseraccesstype { get; set; }
        public IEnumerable<LookupEntity> vw_lookupuserssignature { get; set; }      
        public IEnumerable<LookupEntity> vw_lookupmenuoptiongroups { get; set; }
        public IEnumerable<LookupEntity> vw_lookupdashboardoptionsgroups { get; set; }
        public IEnumerable<LookupEntity> vw_lookupusergroups { get; set; }
        public bool? IsReadOnly { get; set; }
        public bool? IsDeleteDisabled { get; set; }
        [checkValidString]
        public string PasswordHash { get; set; }
    }
}
