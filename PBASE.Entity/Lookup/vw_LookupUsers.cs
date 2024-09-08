using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
namespace PBASE.Entity
{
    public partial class vw_LookupUsers : BaseEntity
    {
        [Key]
        public int? UserId { get; set; }
        public string UserFullName { get; set; }
        public int? SortOrder { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public int? UserTypeId { get; set; }
    }
}

