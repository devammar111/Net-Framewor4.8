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
    public partial class InternalFilterHeaderViewModel : BaseViewModel
    {


        public int? InternalFilterHeaderId { get; set; }
        public int? UserId { get; set; }
        [checkValidString]
        public string FilterName { get; set; }
        [checkValidString]
        public string Role { get; set; }
        [checkValidString]
        public string FilterValue { get; set; }
    }
}