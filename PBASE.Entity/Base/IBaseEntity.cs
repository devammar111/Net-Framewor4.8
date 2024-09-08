using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace PBASE.Entity
{
    public interface IBaseEntity
    {
        [NotMapped]
        FormMode FormMode { get; set; }
    }

}
