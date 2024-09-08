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
    public partial class vw_LookupUserGroupRepository : RepositoryBase<vw_LookupUserGroup>, Ivw_LookupUserGroupRepository
    {
        public vw_LookupUserGroupRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
    }
    
    public partial interface Ivw_LookupUserGroupRepository : IRepository<vw_LookupUserGroup>
    {
    }
}

