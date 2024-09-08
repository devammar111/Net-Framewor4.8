using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PBASE.Entity;
using PBASE.Entity.Helper;

namespace PBASE.Entity
{
    public partial class vw_LookupTypeGrid : BaseEntity
    {
        [Key]
        public int? LookupTypeId { get; set; }
        public string LookupTypeText { get; set; }
        public string LookupList { get; set; }
        public bool? IsLocked { get; set; }
        public string LookupViewLabel { get; set; }
        public string LookupViewLabel2 { get; set; }
        public string LookupViewLabel3 { get; set; }
        public string LookupViewLabel4 { get; set; }

        [NotMapped]
        public string EncryptedLookupTypeId { get { return CryptoEngine.Encrypt(LookupTypeId.ToString()); } }
    }
}

