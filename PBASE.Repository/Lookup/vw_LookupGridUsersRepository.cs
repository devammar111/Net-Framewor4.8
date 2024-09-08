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
    public partial class vw_LookupGridUsersRepository : RepositoryBase<vw_LookupGridUsers>, Ivw_LookupGridUsersRepository
    {
        public vw_LookupGridUsersRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
    }
    
    public partial interface Ivw_LookupGridUsersRepository : IRepository<vw_LookupGridUsers>
    {
    }
}

