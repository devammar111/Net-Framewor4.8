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
    public partial class vw_LookupGridUserAccessTypeRepository : RepositoryBase<vw_LookupGridUserAccessType>, Ivw_LookupGridUserAccessTypeRepository
    {
        public vw_LookupGridUserAccessTypeRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
    }
    
    public partial interface Ivw_LookupGridUserAccessTypeRepository : IRepository<vw_LookupGridUserAccessType>
    {
    }
}

