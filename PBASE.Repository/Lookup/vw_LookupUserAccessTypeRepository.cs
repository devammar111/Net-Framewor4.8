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
    public partial class vw_LookupUserAccessTypeRepository : RepositoryBase<vw_LookupUserAccessType>, Ivw_LookupUserAccessTypeRepository
    {
        public vw_LookupUserAccessTypeRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
    }
    
    public partial interface Ivw_LookupUserAccessTypeRepository : IRepository<vw_LookupUserAccessType>
    {
    }
}

