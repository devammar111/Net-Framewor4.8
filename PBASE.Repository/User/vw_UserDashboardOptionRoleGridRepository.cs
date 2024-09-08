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
    public partial class vw_UserDashboardOptionRoleGridRepository : RepositoryBase<vw_UserDashboardOptionRoleGrid>, Ivw_UserDashboardOptionRoleGridRepository
    {
        public vw_UserDashboardOptionRoleGridRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
        
    }
    
    public partial interface Ivw_UserDashboardOptionRoleGridRepository : IRepository<vw_UserDashboardOptionRoleGrid>
    {
    }
}

