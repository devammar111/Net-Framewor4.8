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
    public partial class vw_LookupDashboardOptionRepository : RepositoryBase<vw_LookupDashboardOption>, Ivw_LookupDashboardOptionRepository
    {
        public vw_LookupDashboardOptionRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
    }
    
    public partial interface Ivw_LookupDashboardOptionRepository : IRepository<vw_LookupDashboardOption>
    {
    }
}

