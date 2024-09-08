using PBASE.Entity.Enum;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PBASE.Entity
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? UserTypeId { get; set; }
        public int CreatedUserId { get; set; }
        public Nullable<int> UpdatedUserId { get; set; }

        [NotMapped]
        public FormMode FormMode { get; set; }
    }
}

