using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity.Core.Objects;
using Probase.GridHelper;
using PBASE.Repository.Infrastructure;
using PBASE.Entity;
namespace PBASE.Repository
{
    public partial class vw_UserGroupDashboardObjectSubGridRepository : RepositoryBase<vw_UserGroupDashboardObjectSubGrid>, Ivw_UserGroupDashboardObjectSubGridRepository
    {
        public vw_UserGroupDashboardObjectSubGridRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
        
    }
    
    public partial interface Ivw_UserGroupDashboardObjectSubGridRepository : IRepository<vw_UserGroupDashboardObjectSubGrid>
    {
    }
}

