using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PBASE.Entity.Helper;
using System;

namespace PBASE.Entity
{
    public partial class vw_LookupTestType : BaseEntity
    {
        [Key]
        public int? TestTypeId { get; set; }
        public string TestType { get; set; }
        public int? SortOrder { get; set; }
        public bool? IsArchived { get; set; }
        public int? LookupExtraInt { get; set; }
        public string LookupExtraText { get; set; }
        public string LookupNameShort { get; set; }
        public decimal? LookupExtraDecimal { get; set; }
        public int? HistoricId { get; set; }
    }

}
