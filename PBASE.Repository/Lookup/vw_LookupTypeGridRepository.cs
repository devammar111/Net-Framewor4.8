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
    public partial class vw_LookupTypeGridRepository : RepositoryBase<vw_LookupTypeGrid>, Ivw_LookupTypeGridRepository
    {
        public vw_LookupTypeGridRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
    }
    
    public partial interface Ivw_LookupTypeGridRepository : IRepository<vw_LookupTypeGrid>
    {
    }
}

