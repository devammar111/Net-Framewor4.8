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
    public partial class vw_LookupGridUserGroupRepository : RepositoryBase<vw_LookupGridUserGroup>, Ivw_LookupGridUserGroupRepository
    {
        public vw_LookupGridUserGroupRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
    }
    
    public partial interface Ivw_LookupGridUserGroupRepository : IRepository<vw_LookupGridUserGroup>
    {
    }
}

