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
    public partial class InternalGridSettingDefaultViewModel : BaseViewModel
    {


        public int? InternalGridSettingDefaultId { get; set; }
        public int? InternalGridSettingId { get; set; }
        [checkValidString]
        public string StorageKey { get; set; }
    }
}