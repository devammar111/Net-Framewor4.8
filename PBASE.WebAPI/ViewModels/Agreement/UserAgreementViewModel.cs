using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using PBASE.Entity;

namespace PBASE.WebAPI.ViewModels
{
    public partial class UserAgreementViewModel : BaseViewModel
    {
        public int? UserAgreementId { get; set; }
        public int? AgreementId { get; set; }
        public int? UserId { get; set; }
        public bool? IsAcceptDecline { get; set; }
        public DateTime? AcceptDeclineDate { get; set; }
    }
}