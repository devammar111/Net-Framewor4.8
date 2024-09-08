using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class vw_RoleGrid : BaseEntity
    {
        [Key]
        public int? Id { get; set; }
        public string Name { get; set; }

    }
}

