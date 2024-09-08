using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class UserExportLog : TrackingEntity
    {
        [Key]
        public int? UserExportLogId { get; set; }
        public string ApplicationOption { get; set; }
        public string DataSource { get; set; }
        public string SavedFilterName { get; set; }
        public string Filter { get; set; }
        public string ExportFileName { get; set; }
        public bool? IsArchived { get; set; }
        public string IPAddress { get; set; }
        public string Location { get; set; }
    }
}

