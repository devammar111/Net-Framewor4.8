using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
using System.ComponentModel.DataAnnotations.Schema;
namespace PBASE.Entity
{
    public partial class InternalReport : TrackingEntity
    {
        [Key]
            public int InternalReportId { get; set; }
            public string ReportName { get; set; }
            public string ReportSource { get; set; }
            public string Caption { get; set; }
            public string SortColumn { get; set; }
            public bool IsArchived { get; set; }
        }
}

