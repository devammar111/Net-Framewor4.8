using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace PBASE.Entity
{
    public interface ITrackingEntity : ITableEntity
    {
        int CreatedUserId { get; set; }
        DateTime CreatedDate { get; set; }
        Nullable<int> UpdatedUserId { get; set; }
        Nullable<DateTime> UpdatedDate { get; set; }
    }
}
