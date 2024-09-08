using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace PBASE.Entity
{
    public interface ITableEntity : IBaseEntity
    {
        Byte[] RecordTimestamp { get; set; }
    }
}
