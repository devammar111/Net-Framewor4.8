using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class vw_UserMenuOption : BaseEntity
    {
        [Key]
        public int? MenuOptionId { get; set; }
        public int? UserId { get; set; }
        public string MenuOption { get; set; }
        public int? AccessTypeId { get; set; }

    }
}

