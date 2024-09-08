using static PBASE.WebAPI.Helpers.CustomValidationAttribute;

namespace PBASE.WebAPI.ViewModels
{
    public class ErrorLog_ErrorLogViewModel
    {
        [checkValidString]
        public string fileName { get; set; }
        public string level { get; set; }
        [checkValidString]
        public string lineNumber { get; set; }
        [checkValidString]
        public string message { get; set; }
        public  string timestamp { get; set; }
    }
}