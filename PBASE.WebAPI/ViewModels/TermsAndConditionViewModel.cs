using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using PBASE.Entity.Enum;
using PBASE.Entity;

namespace PBASE.WebAPI.ViewModels
{
    public class TermsAndConditionViewModel
    {
        public TermsAndConditionViewModel()
        {

        }
        public bool? IsAcceptDecline { get; set; }
    }
}
