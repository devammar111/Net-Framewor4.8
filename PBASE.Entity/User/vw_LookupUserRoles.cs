using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class vw_LookupUserRoles : BaseEntity
    {
        [Key]
        public string Name { get; set; }
        public int? UserId { get; set; }
        public int? Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

    }
}

