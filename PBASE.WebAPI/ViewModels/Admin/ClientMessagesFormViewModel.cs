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
    public partial class ClientMessagesFormViewModel : BaseViewModel
    {
        
        public int MessageId { get; set; }
        [Display(Name = "Header")]
        [checkValidString]
        public string MessageHeader { get; set; }
        [Display(Name = "Text")]
        [checkValidString]
        public string MessageText { get; set; }
        [Display(Name = "Date")]
        public DateTime? MessageDate { get; set; }
        [Display(Name = "User")]
        public int? UserId { get; set; }
        public IEnumerable<SelectListItem> UserIds { get; set; }
        public bool? IsArchived { get; set; }
    }
}

