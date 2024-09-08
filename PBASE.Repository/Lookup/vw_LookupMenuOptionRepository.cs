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
    public partial class vw_LookupMenuOptionRepository : RepositoryBase<vw_LookupMenuOption>, Ivw_LookupMenuOptionRepository
    {
        public vw_LookupMenuOptionRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
    }
    
    public partial interface Ivw_LookupMenuOptionRepository : IRepository<vw_LookupMenuOption>
    {
    }
}

