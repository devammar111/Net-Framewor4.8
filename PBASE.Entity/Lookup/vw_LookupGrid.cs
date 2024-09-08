using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PBASE.Entity;
using PBASE.Entity.Helper;

namespace PBASE.Entity
{
    public partial class vw_LookupGrid : BaseEntity
    {
        [Key]
        public int? LookupId { get; set; }
        public int? LookupTypeId { get; set; }
        public string LookupName { get; set; }
        public int? SortOrder { get; set; }
        public int? LookupExtraInt { get; set; }
        public string LookupNameType { get; set; }
        public string LookupTypeText { get; set; }
        public string LookupView { get; set; }
        public string LookupViewLabel { get; set; }
        public string LookupNameShort { get; set; }
        public string LookupViewIdField { get; set; }
        public string LookupViewDisplayField { get; set; }
        public bool? IsArchived { get; set; }
        public string LookupNameType2 { get; set; }
        public string LookupView2 { get; set; }
        public string LookupViewLabel2 { get; set; }
        public string LookupViewIdField2 { get; set; }
        public string LookupViewDisplayField2 { get; set; }
        public string LookupNameType3 { get; set; }
        public string LookupView3 { get; set; }
        public string LookupViewLabel3 { get; set; }
        public string LookupViewIdField3 { get; set; }
        public string LookupViewDisplayField3 { get; set; }
        public string LookupNameType4 { get; set; }
        public string LookupView4 { get; set; }
        public string LookupViewLabel4 { get; set; }
        public string LookupViewIdField4 { get; set; }
        public string LookupViewDisplayField4 { get; set; }
        [NotMapped]
        public string EncryptedLookupId { get { return CryptoEngine.Encrypt(LookupId.ToString()); } }
    }
}

