using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PBASE.Entity;
using PBASE.Entity.Helper;

namespace PBASE.Entity
{
    public partial class vw_UserMenuOptionRoleGrid : BaseEntity
    {
        [Key]
        public int? MenuOptionId { get; set; }
        public string MenuOption { get; set; }
        public string RoleNameList { get; set; }
        public string ObjectType { get; set; }
        public string GroupList { get; set; }
        public string ObjectName { get; set; }
        [NotMapped]
        public string EncryptedMenuOptionId { get { return CryptoEngine.Encrypt(MenuOptionId.ToString()); } }
    }
}

