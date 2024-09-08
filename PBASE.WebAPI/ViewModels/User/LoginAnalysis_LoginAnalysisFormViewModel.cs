using System.Collections.Generic;
using PBASE.WebAPI.ViewModels;
using PBASE.WebAPI;
using static PBASE.WebAPI.Helpers.CustomValidationAttribute;

namespace PBASE.WebAPI.ViewModels
{
    public partial class LoginAnalysis_LoginAnalysisFormViewModel : BaseViewModel
    {
        public LoginAnalysis_LoginAnalysisFormViewModel()
        {
        }

        public int? Id { get; set; }
        [checkValidString]
        public string StartDate { get; set; }
        [checkValidString]
        public string EndDate { get; set; }
        
    }

}