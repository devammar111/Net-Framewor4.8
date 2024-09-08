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
    public partial class vw_LookupGridRoleRepository : RepositoryBase<vw_LookupGridRole>, Ivw_LookupGridRoleRepository
    {
        public vw_LookupGridRoleRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
    }
    
    public partial interface Ivw_LookupGridRoleRepository : IRepository<vw_LookupGridRole>
    {
    }
}

