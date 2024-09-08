using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
using PBASE.Entity.Helper;
using System.ComponentModel.DataAnnotations.Schema;
namespace PBASE.Entity
{
    public partial class vw_UserGroupGrid : BaseEntity
    {
        [Key]
        public int? UserGroupId { get; set; }
        public string UserGroupName { get; set; }
        public string RoleName { get; set; }
        public string UserMenuOptionList { get; set; }
        public string UserDashboardOptionList { get; set; }
         public int? AspNetRoleId { get; set; }
      [NotMapped]
        public string EncryptedUserGroupId { get { return CryptoEngine.Encrypt(UserGroupId.ToString()); } }

    }
}

