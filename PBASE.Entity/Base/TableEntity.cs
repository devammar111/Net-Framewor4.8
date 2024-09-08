using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PBASE.Entity
{
    public abstract class TableEntity : BaseEntity, ITableEntity
    {
        [ConcurrencyCheck]
        [Timestamp]
        public Byte[] RecordTimestamp { get; set; }
    }
}
