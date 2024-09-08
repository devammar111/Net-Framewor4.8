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
    public partial class LogFormViewModel : BaseViewModel
    {
        
        [Display(Name = "Log Id")]
        public int? LogId { get; set; }
        [Display(Name = "Log Function Id")]
        public int? LogFunctionId { get; set; }
        [Display(Name = "Log Type Id")]
        public int? LogTypeId { get; set; }
        [Display(Name = "Log Text")]
        [checkValidString]
        public string LogText { get; set; }
        [Display(Name = "Assigned User Id")]
        public int? AssignedUserId { get; set; }
        [Display(Name = "Log Action Id")]
        public int? LogActionId { get; set; }
        [Display(Name = "Log Status Id")]
        public int? LogStatusId { get; set; }
        [Display(Name = "Attachment Id")]
        public int? AttachmentId { get; set; }
        [Display(Name = "Log Function Type Id")]
        public int? LogFunctionTypeId { get; set; }
    }
}

