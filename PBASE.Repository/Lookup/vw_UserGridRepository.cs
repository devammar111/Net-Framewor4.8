using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity.Core.Objects;
using PBASE.Repository.Infrastructure;
using PBASE.Entity;
namespace PBASE.Repository
{
    public partial class vw_UserGridRepository : RepositoryBase<vw_UserGrid>, Ivw_UserGridRepository
    {
        public vw_UserGridRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
    }
    
    public partial interface Ivw_UserGridRepository : IRepository<vw_UserGrid>
    {
    }
}

