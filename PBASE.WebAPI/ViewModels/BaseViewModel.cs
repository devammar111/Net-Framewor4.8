using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using PBASE.Entity.Enum;
using PBASE.WebAPI.Helpers;

namespace PBASE.WebAPI.ViewModels
{
    public abstract class BaseViewModel
    {
        public BaseViewModel()
        {
        }

        [HiddenInput(DisplayValue = false)]
        [Display(AutoGenerateField = false)]
        public FormMode FormMode { get; set; }

        public string FormName { get; set; }
        public string GridName { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Display(AutoGenerateField = false)]
        public int CreatedUserId { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Display(AutoGenerateField = false)]
        [NotMapped]
        public string Message { get; set; }

        public Byte[] RecordTimestamp { get; set; }

        [Display(Name = "Create another Part", AutoGenerateField = false)]
        public bool IsCreateAnother { get; set; }

        public string RequestValidation { get { return UserHelper.GetRequestToken(); } }
    }
}
