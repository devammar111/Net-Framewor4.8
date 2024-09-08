using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;
using PBASE.WebAPI.ViewModels;
using PBASE.Entity;
using static PBASE.WebAPI.Helpers.CustomValidationAttribute;

namespace PBASE.WebAPI.ViewModels
{
    public partial class InternalReportViewModel : BaseViewModel
    {


        public int InternalReportId { get; set; }
        [checkValidString]
        public string ReportName { get; set; }
        [checkValidString]
        public string ReportSource { get; set; }
        [checkValidString]
        public string Caption { get; set; }
        [checkValidString]
        public string SortColumn { get; set; }
        public bool IsArchived { get; set; }
    }
}