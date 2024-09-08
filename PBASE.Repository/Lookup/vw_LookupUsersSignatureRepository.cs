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
    public partial class vw_LookupUsersSignatureRepository : RepositoryBase<vw_LookupUsersSignature>, Ivw_LookupUsersSignatureRepository
    {
        public vw_LookupUsersSignatureRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
    }
    
    public partial interface Ivw_LookupUsersSignatureRepository : IRepository<vw_LookupUsersSignature>
    {
    }
}

