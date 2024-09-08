using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class ApplicationInformation : TrackingEntity
    {
        [Key]
        public int? ApplicationInformationId { get; set; }
        public string CompanyName { get; set; }
        public string ApplicationName { get; set; }
        public string ApplicationShortName { get; set; }
        public bool? IsArchived { get; set; }
        public int? StandardExportCount { get; set; }
        public int? MaximumExportCount { get; set; }
        public int? CSVExportCount { get; set; }
        public int? WarningExportCount { get; set; }
        public int? LoginLimit { get; set; }

    }
}

