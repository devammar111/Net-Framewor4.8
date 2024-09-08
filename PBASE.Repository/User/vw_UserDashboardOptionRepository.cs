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
    public partial class vw_UserDashboardOptionRepository : RepositoryBase<vw_UserDashboardOption>, Ivw_UserDashboardOptionRepository
    {
        public vw_UserDashboardOptionRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
        
    }
    
    public partial interface Ivw_UserDashboardOptionRepository : IRepository<vw_UserDashboardOption>
    {
    }
}

