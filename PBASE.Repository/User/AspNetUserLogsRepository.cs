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
    public partial class AspNetUserLogsRepository : RepositoryBase<AspNetUserLogs>, IAspNetUserLogsRepository
    {
        public AspNetUserLogsRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
        
    }
    
    public partial interface IAspNetUserLogsRepository : IRepository<AspNetUserLogs>
    {
    }
}

