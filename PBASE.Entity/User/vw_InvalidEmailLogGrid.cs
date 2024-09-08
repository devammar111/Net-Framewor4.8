using System;
using System.ComponentModel.DataAnnotations;

namespace PBASE.Entity
{
    public partial class vw_InvalidEmailLogGrid : BaseEntity
    {
        [Key]
        public int? Id { get; set; }
        public string Email { get; set; }
        public int? AccessFailedCount { get; set; }
        public DateTime? LastAccessDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
