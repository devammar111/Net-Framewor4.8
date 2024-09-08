using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;
using PBASE.WebAPI.ViewModels;
using PBASE.Entity;

namespace PBASE.WebAPI.ViewModels
{
    public partial class UserGroupMenuOptionFormViewModel : BaseViewModel
    {

        public UserGroupMenuOptionFormViewModel()
        {
        }

        public int? UserGroupMenuOptionId { get; set; }
        public int? UserGroupId { get; set; }
        public int? MenuOptionId { get; set; }
        public bool? IsArchived { get; set; }
        public bool? IsReadOnly { get; set; }
        public bool? IsDeleteAllowed { get; set; }
        public int? AccessTypeId { get; set; }

        public IEnumerable<vw_UserGroupObjectSubGrid> Entity { get; set; }
    }
}

