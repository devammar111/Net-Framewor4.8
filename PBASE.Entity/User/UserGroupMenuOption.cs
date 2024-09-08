using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class UserGroupMenuOption : TrackingEntity
    {
        [Key]
        public int? UserGroupMenuOptionId { get; set; }
        public int? UserGroupId { get; set; }
        public int? MenuOptionId { get; set; }
        public bool? IsArchived { get; set; }
        public bool? IsReadOnly { get; set; }
        public bool? IsDeleteAllowed { get; set; }
        public int? AccessTypeId { get; set; }

    }
}

