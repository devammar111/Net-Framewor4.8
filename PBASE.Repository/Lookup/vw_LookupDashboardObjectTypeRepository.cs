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
    public partial class vw_LookupDashboardObjectTypeRepository : RepositoryBase<vw_LookupDashboardObjectType>, Ivw_LookupDashboardObjectTypeRepository
    {
        public vw_LookupDashboardObjectTypeRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
    }
    
    public partial interface Ivw_LookupDashboardObjectTypeRepository : IRepository<vw_LookupDashboardObjectType>
    {
    }
}

