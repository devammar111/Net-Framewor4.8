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
    public partial class vw_LookupObjectRepository : RepositoryBase<vw_LookupObject>, Ivw_LookupObjectRepository
    {
        public vw_LookupObjectRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
    }
    
    public partial interface Ivw_LookupObjectRepository : IRepository<vw_LookupObject>
    {
    }
}

