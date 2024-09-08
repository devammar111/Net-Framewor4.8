using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
using static PBASE.WebAPI.Helpers.CustomValidationAttribute;

namespace PBASE.WebAPI.ViewModels
{
    public partial class TestSubViewModel : BaseViewModel
    {
        public int? TestSubId { get; set; }
        public int? TestId { get; set; }
        [DisplayName("Test Sub Text 1")]
        [checkValidString]
        [StringLength(50)]
        public string TestSubText1 { get; set; }
        [DisplayName("Test Sub Text 2")]
        [checkValidString]
        [StringLength(10)]
        public string TestSubText2 { get; set; }
        [DisplayName("Test Sub Text 3")]
        [checkValidString]
        [StringLength(100)]
        public string TestSubText3 { get; set; }
        [DisplayName("Test Text 4")]
        [checkValidString]
        [checkReadOnly(-9955)]
        public string TestSubText4 { get; set; }
        public int? TestSubType1Id { get; set; }
        public int? TestSubType2Id { get; set; }
        public int? TestSubType3Id { get; set; }
        public DateTime? TestSubDate1 { get; set; }
        public DateTime? TestSubDate2 { get; set; }
        public DateTime? TestSubDate3 { get; set; }
        public bool? IsTestSub1 { get; set; }
        public bool? IsTestSub2 { get; set; }
        public bool? IsTestSub3 { get; set; }
        public int? TestAttachmentId { get; set; }
        public bool? IsArchived { get; set; }
        [checkValidString]
        public string EncTestId { get; set; }
        public IEnumerable<LookupEntity> vw_LookupTestType { get; set; }

    }
}