using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PBASE.Entity.Helper;
using System;

namespace PBASE.Entity
{
    public partial class vw_LookupGridTestType : BaseEntity
    {
        [Key]
        public string TestType { get; set; }
        public string TestTypeDisplay { get; set; }
        public int? TestTypeId { get; set; }
    }

}
