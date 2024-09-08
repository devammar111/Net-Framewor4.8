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
    public partial class vw_LookupAccessTypeRepository : RepositoryBase<vw_LookupAccessType>, Ivw_LookupAccessTypeRepository
    {
        public vw_LookupAccessTypeRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
    }
    
    public partial interface Ivw_LookupAccessTypeRepository : IRepository<vw_LookupAccessType>
    {
    }
}

