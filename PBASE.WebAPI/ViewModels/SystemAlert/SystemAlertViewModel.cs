using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
using static PBASE.WebAPI.Helpers.CustomValidationAttribute;

namespace PBASE.WebAPI.ViewModels
{
    public partial class SystemAlertViewModel : BaseViewModel
    {

        public int? SystemAlertId { get; set; }
        public int? AlertTypeId { get; set; }
        [DisplayName("Alert Text")]
        public string AlertText { get; set; }
        [DisplayName("Close Text")]
        public string CloseText { get; set; }
        [DisplayName("Open Text")]
        public string OpenText { get; set; }
        public DateTime? ClosedDateTime { get; set; }
        public DateTime? OpenDateTime { get; set; }
        public int? WarningTime { get; set; }
        public int? OpenMessageTime { get; set; }
        public bool? IsArchived { get; set; }
        public IEnumerable<LookupEntity> vw_LookupAlertType { get; set; }
    }
}