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
    public partial class vw_AspNetUserLogsGridRepository : RepositoryBase<vw_AspNetUserLogsGrid>, Ivw_AspNetUserLogsGridRepository
    {
        public vw_AspNetUserLogsGridRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
        
    }
    
    public partial interface Ivw_AspNetUserLogsGridRepository : IRepository<vw_AspNetUserLogsGrid>
    {
    }
}

