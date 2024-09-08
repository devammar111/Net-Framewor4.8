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
    public partial class vw_MessageGridRepository : RepositoryBase<vw_MessageGrid>, Ivw_MessageGridRepository
    {
        public vw_MessageGridRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
    }
    
    public partial interface Ivw_MessageGridRepository : IRepository<vw_MessageGrid>
    {
    }
}

