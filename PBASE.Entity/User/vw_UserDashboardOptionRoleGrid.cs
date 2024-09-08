using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
using PBASE.Entity.Helper;
using System.ComponentModel.DataAnnotations.Schema;
namespace PBASE.Entity
{
    public partial class vw_UserDashboardOptionRoleGrid : BaseEntity
    {
        [Key]
        public int? DashboardOptionId { get; set; }
        public string DashboardOption { get; set; }
        public string RoleNameList { get; set; }
        [NotMapped]
        public string EncryptedDashboardOptionId { get { return CryptoEngine.Encrypt(DashboardOptionId.ToString()); } }
    }
}

