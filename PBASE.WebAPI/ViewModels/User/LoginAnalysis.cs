using System.Collections.Generic;
using PBASE.WebAPI.ViewModels;
using PBASE.WebAPI;
using System;
using static PBASE.WebAPI.Helpers.CustomValidationAttribute;

namespace PBASE.WebAPI.ViewModels
{
    public partial class LoginAnalysis : BaseViewModel
    {
        public LoginAnalysis()
        {
        }
        [checkValidString]
        public string RequestType { get; set; }
        public DateTime? CreatedDate { get; set; }
        public TimeSpan? Time { get; set; }
        [checkValidString]
        public string IPAddress { get; set; }
        [checkValidString]
        public string Location { get; set; }
        public bool? IsStatus { get; set; }
    }

}