﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class vw_LookupObjectType : BaseEntity
    {
        [Key]
        public int? ObjectTypeId { get; set; }
        public string ObjectType { get; set; }
        public int? SortOrder { get; set; }
        public bool? IsArchived { get; set; }
        public int? LookupExtraInt { get; set; }
        public string LookupExtraText { get; set; }
        public string LookupNameShort { get; set; }
        public decimal? LookupExtraDecimal { get; set; }
        public int? HistoricId { get; set; }
    }
}
