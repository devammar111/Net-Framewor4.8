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
    public partial class vw_LookupRoleRepository : RepositoryBase<vw_LookupRole>, Ivw_LookupRoleRepository
    {
        public vw_LookupRoleRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
    }
    
    public partial interface Ivw_LookupRoleRepository : IRepository<vw_LookupRole>
    {
    }
}

