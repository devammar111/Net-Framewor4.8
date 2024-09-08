using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class InternalGridSetting : TrackingEntity
    {
        [Key]
        public int InternalGridSettingId { get; set; }
        public string StorageKey { get; set; }
        public string StorageData { get; set; }
        public bool? IsArchived { get; set; }
        public string StateName { get; set; }
        public bool? IsGlobal { get; set; }

        [NotMapped]
        public bool? IsDefalut { get; set; }

    }
}

