using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PBASE.Entity;
using PBASE.Entity.Helper;

namespace PBASE.Entity
{
    public partial class vw_SystemAlertGrid : BaseEntity
    {
        [Key]
        public int? SystemAlertId { get; set; }
        public string AlertType { get; set; }
        public DateTime? ClosedDateTime { get; set; }
        public DateTime? OpenDateTime { get; set; }
        public int? WarningTime { get; set; }
        public int? OpenMessageTime { get; set; }
        [NotMapped]
        public string EncryptedSystemAlertId { get { return CryptoEngine.Encrypt(SystemAlertId.ToString()); } }
    }
}

