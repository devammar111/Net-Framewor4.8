using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace PBASE.Entity
{
    public abstract class BaseEntity : IBaseEntity
    {
        [NotMapped]
        public FormMode FormMode { get; set; }
    }
}
