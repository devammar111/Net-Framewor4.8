using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PBASE.Entity;
using PBASE.Entity.Helper;

namespace PBASE.Entity
{
    public partial class vw_AspNetUserLogsGrid : BaseEntity
    {
        [Key]
        public int? Id { get; set; }
        public string FullName { get; set; }
        public string username { get; set; }
        public int? TotalLogin { get; set; }
        public int? LoginSuccess { get; set; }
        public int? LoginFailure { get; set; }
        public int? PasswordResetTotal { get; set; }
        public int? PasswordResetSuccess { get; set; }
        public int? PasswordResetFailure { get; set; }
        public int? ChangePasswordSuccess { get; set; }
        public int? ChangePasswordFailure { get; set; }
        public DateTime? SuccessCreatedDate { get; set; }
        public DateTime? FailedCreatedDate { get; set; }
        public string IPAddress { get; set; }
        public string Location { get; set; }
        [NotMapped]
        public string SuccessCreatedDateString { get; set; }
        [NotMapped]
        public string FailedCreatedDateString { get; set; }
        [NotMapped]
        public string EncryptedId { get { return CryptoEngine.Encrypt(Id.ToString()); } }
    }
}

