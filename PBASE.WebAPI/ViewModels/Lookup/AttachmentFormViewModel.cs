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
    public partial class AttachmentFormViewModel : BaseViewModel
    {


        public int? AttachmentId { get; set; }
        [checkValidString]
        public string AttachmentFileName { get; set; }
        public decimal? AttachmentFileSize { get; set; }
        [checkValidString]
        public string AttachmentDescription { get; set; }
        [checkValidString]
        public string AttachmentFileHandle { get; set; }
        [checkValidString]
        public string AttachmentFileType { get; set; }
    }
}