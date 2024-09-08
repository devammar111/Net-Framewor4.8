using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
using PBASE.Entity.Helper;
using System.ComponentModel.DataAnnotations.Schema;
namespace PBASE.Entity
{
    public partial class vw_UserGroupObjectSubGrid : BaseEntity
    {
        [Key]
        public Guid UniqueId { get; set; }
        public int? UserGroupMenuOptionId { get; set; }
        public int? LookupExtraInt { get; set; }
        public int? UserGroupId { get; set; }
        public int? ObjectId { get; set; }
        public int? MenuId { get; set; }
        public int? TabId { get; set; }
        public string ObjectName { get; set; }
        public string ObjectType { get; set; }
        public int? AccessTypeId { get; set; }
        public string AccessType { get; set; }
        public string MenuName { get; set; }
        public int? SortOrder { get; set; }
        [NotMapped]
        public string EncryptedUserGroupId { get { return CryptoEngine.Encrypt(UserGroupId.ToString()); } }
    }

}

