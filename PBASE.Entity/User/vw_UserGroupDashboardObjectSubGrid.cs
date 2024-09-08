using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
using PBASE.Entity.Helper;
using System.ComponentModel.DataAnnotations.Schema;
namespace PBASE.Entity
{
    public partial class vw_UserGroupDashboardObjectSubGrid : BaseEntity
    {
        [Key]
        public Guid UniqueId { get; set; }
        public int? UserGroupDashboardOptionId { get; set; }
        public int? LookupExtraInt { get; set; }
        public int? UserGroupId { get; set; }
        public int? DashboardObjectId { get; set; }
        public string DashboardObject { get; set; }
        public string DashboardObjectType { get; set; }
        public int? AccessTypeId { get; set; }
        public string AccessType { get; set; }
        [NotMapped]
        public string EncryptedUserGroupId { get { return CryptoEngine.Encrypt(UserGroupId.ToString()); } }
    }

}

