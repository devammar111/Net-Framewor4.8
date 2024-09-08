using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class UserClaims : TrackingEntity
    {
        [Key]
        public int? UserAccountID { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }

    }
}

