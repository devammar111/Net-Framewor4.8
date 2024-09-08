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
    public partial class vw_RoleGridRepository : RepositoryBase<vw_RoleGrid>, Ivw_RoleGridRepository
    {
        public vw_RoleGridRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
        
    }
    
    public partial interface Ivw_RoleGridRepository : IRepository<vw_RoleGrid>
    {
    }
}

