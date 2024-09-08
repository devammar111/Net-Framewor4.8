using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PBASE.Entity
{
    public abstract class TrackingEntity : TableEntity, ITrackingEntity
    {
        public int CreatedUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> UpdatedUserId { get; set; }
        public Nullable<DateTime> UpdatedDate { get; set; }
    }
}
