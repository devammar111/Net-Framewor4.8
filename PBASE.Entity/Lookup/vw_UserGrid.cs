using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PBASE.Entity;
using PBASE.Entity.Helper;

namespace PBASE.Entity
{
    public partial class vw_UserGrid : BaseEntity
    {
        [Key]

        public int? Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool? LockoutEnabled { get; set; }
        public string UserGroupName { get; set; }
        public string AssignedRole { get; set; }
        public string UserAccessType { get; set; }
        public bool? IsReadOnly { get; set; }
        public bool? IsDeleteDisabled { get; set; }
        public int? UserTypeId { get; set; }
      [NotMapped]
        public string EncryptedUserId { get { return CryptoEngine.Encrypt(Id.ToString()); } }
    }
}

