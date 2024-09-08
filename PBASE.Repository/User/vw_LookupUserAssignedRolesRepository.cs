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
    public partial class vw_LookupUserAssignedRolesRepository : RepositoryBase<vw_LookupUserAssignedRoles>, Ivw_LookupUserAssignedRolesRepository
    {
        public vw_LookupUserAssignedRolesRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
        
    }
    
    public partial interface Ivw_LookupUserAssignedRolesRepository : IRepository<vw_LookupUserAssignedRoles>
    {
    }
}

