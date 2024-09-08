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
    public partial class vw_LookupFromEmailAddressRepository : RepositoryBase<vw_LookupFromEmailAddress>, Ivw_LookupFromEmailAddressRepository
    {
        public vw_LookupFromEmailAddressRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
    }
    
    public partial interface Ivw_LookupFromEmailAddressRepository : IRepository<vw_LookupFromEmailAddress>
    {
    }
}

