using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
using static PBASE.WebAPI.Helpers.CustomValidationAttribute;

namespace PBASE.WebAPI.ViewModels
{
    public partial class ApplicationInformationFormViewModel : BaseViewModel
    {
        public int? ApplicationInformationId { get; set; }
        public string CompanyName { get; set; }
        public string ApplicationName { get; set; }
        public string ApplicationShortName { get; set; }
        public bool? IsArchived { get; set; }
        public int? StandardExportCount { get; set; }
        public int? MaximumExportCount { get; set; }
        public int? CSVExportCount { get; set; }
        public int? WarningExportCount { get; set; }
        public string EncApplicationInformationId { get; set; }
        public int? LoginLimit { get; set; }
    }
}