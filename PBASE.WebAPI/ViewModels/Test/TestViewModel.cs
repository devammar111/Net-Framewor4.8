using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
using static PBASE.WebAPI.Helpers.CustomValidationAttribute;

namespace PBASE.WebAPI.ViewModels
{
    public partial class TestViewModel : BaseViewModel
    {
        public int? TestId { get; set; }
        [DisplayName("Test Text 1")]
        [checkValidString]
        [StringLength(50)]
        public string TestText1 { get; set; }
        [DisplayName("Test Text 2")]
        [checkValidString]
        [StringLength(50)]
        [checkReadOnly(-9956)]
        public string TestText2 { get; set; }
        [DisplayName("Test Text 3")]
        [checkValidString]
        [StringLength(100)]
        public string TestText3 { get; set; }
        [DisplayName("Test Text 4")]
        [checkValidString]
        [checkReadOnly(-9955)]
        public string TestText4 { get; set; }

        public int? TestType1Id { get; set; }
        public int? TestType2Id { get; set; }
        public int? TestType3Id { get; set; }
        public DateTime? TestDate1 { get; set; }
        public DateTime? TestDate2 { get; set; }
        public DateTime? TestDate3 { get; set; }
        public bool? IsTest1 { get; set; }
        public bool? IsTest2 { get; set; }
        public bool? IsTest3 { get; set; }
        public int? TestAttachmentId { get; set; }
        public bool? IsArchived { get; set; }
        
        public IEnumerable<LookupEntity> vw_LookupTestType { get; set; }

    }
}