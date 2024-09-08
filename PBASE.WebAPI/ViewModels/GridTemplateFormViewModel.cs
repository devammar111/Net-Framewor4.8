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
    public partial class GridTemplateFormViewModel : BaseViewModel
    {
        
        public int FilterHeaderId { get; set; }
        [Display(Name = "Role")]
        public Int32? RoleId { get; set; }
        [Display(Name = "User")]
        public Int32? UserId { get; set; }
        [checkValidString]
        [Display(Name = "Grid Name")]
        public string FilterGridName { get; set; }
        [Display(Name = "Filter Name")]
        [checkValidString]

        public string FilterName { get; set; }
        [Display(Name = "Filter Type")]
        [checkValidString]
        public string FilterType { get; set; }
        [checkValidString]
        public string FilterValue { get; set; }
        public IList<SelectListItem> Users { get; set; }
        public IList<SelectListItem> Roles { get; set; }
        public IList<SelectListItem> FilterTypes { get; set; }
    }
}

