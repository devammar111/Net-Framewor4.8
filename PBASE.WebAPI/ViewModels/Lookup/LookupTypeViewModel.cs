using System;
using static PBASE.WebAPI.Helpers.CustomValidationAttribute;

namespace PBASE.WebAPI.ViewModels
{
    public partial class LookupTypeViewModel : BaseViewModel
    {

        public int? LookupTypeId { get; set; }
        public string LookupTypeText { get; set; }
        public bool? IsLocked { get; set; }
        [checkValidString]
        public string LookupView { get; set; }
        [checkValidString]
        public string LookupViewLabel { get; set; }
        [checkValidString]
        public string LookupViewIdField { get; set; }
        [checkValidString]
        public string LookupViewDisplayField { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}