using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;
namespace PBASE.Entity
{
    public partial class InternalReportField : TrackingEntity
    {
        [Key]
        public int InternalReportFieldId { get; set; }
        public int InternalReportId { get; set; }
        public string ColumnName { get; set; }
        public string DropdownSource { get; set; }
        public string DropdownSourceColumn { get; set; }

        [NotMapped]
        public string Type { get; set; }

        [NotMapped]
        public bool IsDropdown { get; set; }

        [NotMapped]
        public bool IsNullable { get; set; }

        [NotMapped]
        public string DisplayName { get; set; }
    }
}

