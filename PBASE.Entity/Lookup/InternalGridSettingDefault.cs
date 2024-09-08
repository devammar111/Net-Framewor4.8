using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class InternalGridSettingDefault : TrackingEntity
    {
        [Key]
        public int InternalGridSettingDefaultId { get; set; }
        public int InternalGridSettingId { get; set; }
        public string StorageKey { get; set; }
        public bool? IsArchived { get; set; }
    }
}

