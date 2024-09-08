using System.Collections.Generic;
using PBASE.Entity;

namespace PBASE.WebAPI.ViewModels
{
    public partial class UserGroupDashboardTableViewModel : BaseViewModel
    {
        public IEnumerable<vw_UserGroupDashboardObjectSubGrid> Rows { get; set; }
        public IEnumerable<vw_UserGroupDashboardObjectSubGrid> UpdatedRows { get; set; }
        public IEnumerable<LookupEntity> vw_LookupAccessType { get; set; }
    }
    
}
