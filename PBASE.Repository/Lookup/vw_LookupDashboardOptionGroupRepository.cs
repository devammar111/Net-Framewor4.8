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
    public partial class vw_LookupDashboardOptionGroupRepository : RepositoryBase<vw_LookupDashboardOptionGroup>, Ivw_LookupDashboardOptionGroupRepository
    {
        public vw_LookupDashboardOptionGroupRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
    }
    
    public partial interface Ivw_LookupDashboardOptionGroupRepository : IRepository<vw_LookupDashboardOptionGroup>
    {
    }
}

