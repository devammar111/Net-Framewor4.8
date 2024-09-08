using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
using static PBASE.WebAPI.Helpers.CustomValidationAttribute;

namespace PBASE.WebAPI.ViewModels
{
    public partial class TestNoteViewModel : BaseViewModel
    {
        public int? TestNoteId { get; set; }
        public int? TestId { get; set; }
        public int? TestNoteTypeId { get; set; }
        [DisplayName("Test Note")]
        [checkValidString]
        public string TestNoteText { get; set; }
        public bool? IsArchived { get; set; }
        public IEnumerable<LookupEntity> vw_LookupTestType { get; set; }
        [checkValidString]
        public string EncTestId { get; set; }

    }
}