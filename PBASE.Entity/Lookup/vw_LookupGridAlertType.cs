using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class vw_LookupGridAlertType : BaseEntity
    {
        [Key]
        public string AlertType { get; set; }
        public string AlertTypeDisplay { get; set; }
    }
}

