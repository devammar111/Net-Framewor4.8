using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class vw_LookupUsersSignature : BaseEntity
    {
        [Key]
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string UserFullName { get; set; }
        public int? SortOrder { get; set; }
        public bool? IsAccountClosed { get; set; }
        public string EmailSignature { get; set; }
        public int? SignatureImageAttachmentId { get; set; }
    }
}

