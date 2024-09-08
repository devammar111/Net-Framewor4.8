using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace PBASE.Entity.Enum
{
    public enum EmailType
    {
        None = 0,
        System = -10000,
        Other = -9999
    }
}