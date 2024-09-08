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
    public partial class vw_LookupUserRolesRepository : RepositoryBase<vw_LookupUserRoles>, Ivw_LookupUserRolesRepository
    {
        public vw_LookupUserRolesRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
        
    }
    
    public partial interface Ivw_LookupUserRolesRepository : IRepository<vw_LookupUserRoles>
    {
    }
}

