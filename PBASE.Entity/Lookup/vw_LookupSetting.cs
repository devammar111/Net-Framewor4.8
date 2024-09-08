using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class vw_LookupSetting : BaseEntity
    {
        [Key]
        public int? InternalGridSettingId { get; set; }
        public string StateName { get; set; }
        public string StorageKey { get; set; }
        public int? UserId { get; set; }
        public int? UserTypeId { get; set; }
        public int? SortOrder { get; set; }
        public int? IsGlobal { get; set; }
    }

}

