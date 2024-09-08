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
    public partial class TemplateManagement_TemplateFormViewModel : BaseViewModel
    {
        
        [Display(Name = "Template Id")]
        public int? TemplateId { get; set; }
        public int? AttachmentId { get; set; }
        [Display(Name = "Type")]
        public int? TemplateTypeId { get; set; }
        public IEnumerable<SelectListItem> TemplateTypeIds { get; set; }
        [Display(Name = "Description")]
        [checkValidString]
        [StringLength(2000)]
        public string Description { get; set; }
        [checkValidString]
        public string AttachmentFileHandle { get; set; }
        [checkValidString]
        public string UnsavedFileHandle { get; set; }
        [Display(Name = "Attachment")]
        [checkValidString]
        public string UploadedFile { get; set; }
    }
}

