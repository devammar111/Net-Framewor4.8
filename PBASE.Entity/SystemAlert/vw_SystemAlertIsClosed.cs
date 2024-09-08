using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PBASE.Entity;
using PBASE.Entity.Helper;

namespace PBASE.Entity
{
    public partial class vw_SystemAlertIsClosed : BaseEntity
    {
        [Key]
        public int? SystemAlertId { get; set; }
        public int? AlertTypeId { get; set; }
        public string CloseText { get; set; }
        public int? LookupExtraInt { get; set; }
        public bool? IsClosed { get; set; }
        [NotMapped]
        public string EncryptedAlertTypeId { get { return CryptoEngine.Encrypt(AlertTypeId.ToString()); } }
    }
}

