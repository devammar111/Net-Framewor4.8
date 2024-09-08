using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class Lookup : TrackingEntity
    {
        [Key]
        public int? LookupId { get; set; }
        public int? LookupTypeId { get; set; }
        public string LookupName { get; set; }
        public string LookupNameShort { get; set; }
        public int? LookupExtraInt { get; set; }
        public string LookupExtraText { get; set; }
        public decimal? LookupExtraDecimal { get; set; }
        public DateTime? LookupExtraDate { get; set; }
        public int? SortOrder { get; set; }
        public int? HistoricId { get; set; }
        public bool? IsArchived { get; set; }
        public int? LookupExtraInt2 { get; set; }
        public int? LookupExtraInt3 { get; set; }
        public int? LookupExtraInt4 { get; set; }

    }

    public class AllLookupView
    {
        public int? Value { get; set; }
        public string Text { get; set; }
        public bool? disabled { get; set; }
        public string GroupBy { get; set; }
    }
}

