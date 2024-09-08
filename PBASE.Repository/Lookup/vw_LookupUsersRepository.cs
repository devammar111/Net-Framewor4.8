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
    public partial class vw_LookupUsersRepository : RepositoryBase<vw_LookupUsers>, Ivw_LookupUsersRepository
    {
        public vw_LookupUsersRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
    }
    
    public partial interface Ivw_LookupUsersRepository : IRepository<vw_LookupUsers>
    {
    }
}

