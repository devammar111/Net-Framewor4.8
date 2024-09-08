using System.Collections.Generic;
using PBASE.Entity;

namespace PBASE.WebAPI.ViewModels
{
    public partial class UserGroupTableViewModel : BaseViewModel
    {
        public IEnumerable<vw_UserGroupObjectSubGrid> Rows { get; set; }
        public IEnumerable<vw_UserGroupObjectSubGrid> UpdatedRows { get; set; }
        public IEnumerable<LookupEntity> vw_LookupAccessType { get; set; }
    }
    
}
