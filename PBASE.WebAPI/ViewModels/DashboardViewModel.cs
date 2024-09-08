using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using PBASE.Entity.Enum;
using PBASE.Entity;
using static PBASE.WebAPI.Helpers.CustomValidationAttribute;

namespace PBASE.WebAPI.ViewModels
{
    public class DashboardViewModel
    {
        public DashboardViewModel()
        {

        }
        public int? PageComplete { get; set; }
        public decimal? CompletePercentage { get; set; }
        [checkValidString]
        public string DaysToGo { get; set; }
        [checkValidString]
        public string SubmissionDate { get; set; }

    }
}
