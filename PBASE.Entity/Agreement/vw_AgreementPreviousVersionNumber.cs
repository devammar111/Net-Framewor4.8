using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PBASE.Entity;
using PBASE.Entity.Helper;

namespace PBASE.Entity
{
    public partial class vw_AgreementPreviousVersionNumber : BaseEntity
    {
        [Key]
      public int? AgreementId { get; set; }
      public decimal? VersionNo { get; set; }
    }
}

