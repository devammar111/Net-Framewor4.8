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
    public partial class vw_LookupMenuOptionGroupRepository : RepositoryBase<vw_LookupMenuOptionGroup>, Ivw_LookupMenuOptionGroupRepository
    {
        public vw_LookupMenuOptionGroupRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
    }
    
    public partial interface Ivw_LookupMenuOptionGroupRepository : IRepository<vw_LookupMenuOptionGroup>
    {
    }
}

