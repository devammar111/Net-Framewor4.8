using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class LookupType : TableEntity, ITrackingEntity
    {
        [Key]
        public int? LookupTypeId { get; set; }
        public string LookupTypeText { get; set; }
        public bool? IsLocked { get; set; }
        public string LookupView { get; set; }
        public string LookupViewLabel { get; set; }
        public string LookupViewIdField { get; set; }
        public string LookupViewDisplayField { get; set; }
        public bool? IsArchived { get; set; }
        public int CreatedUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string LookupView2 { get; set; }
        public string LookupViewLabel2 { get; set; }
        public string LookupViewIdField2 { get; set; }
        public string LookupViewDisplayField2 { get; set; }
        public string LookupView3 { get; set; }
        public string LookupViewLabel3 { get; set; }
        public string LookupViewIdField3 { get; set; }
        public string LookupViewDisplayField3 { get; set; }
        public string LookupView4 { get; set; }
        public string LookupViewLabel4 { get; set; }
        public string LookupViewIdField4 { get; set; }
        public string LookupViewDisplayField4 { get; set; }
    }
}

