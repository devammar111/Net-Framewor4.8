using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PBASE.Entity;
using PBASE.Entity.Helper;

namespace PBASE.Entity
{
    public partial class vw_SystemAlertMessages : BaseEntity
    {
        [Key]
        public int? SystemAlertId { get; set; }
        public int? AlertTypeId { get; set; }
        public string AlertText { get; set; }
        public bool? IsRead { get; set; }
        [NotMapped]
        public string EncryptedSystemAlertId { get { return CryptoEngine.Encrypt(SystemAlertId.ToString()); } }
    }
}

