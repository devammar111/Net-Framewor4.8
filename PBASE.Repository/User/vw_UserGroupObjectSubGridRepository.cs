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
    public partial class vw_UserGroupObjectSubGridRepository : RepositoryBase<vw_UserGroupObjectSubGrid>, Ivw_UserGroupObjectSubGridRepository
    {
        public vw_UserGroupObjectSubGridRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
        
    }
    
    public partial interface Ivw_UserGroupObjectSubGridRepository : IRepository<vw_UserGroupObjectSubGrid>
    {
    }
}

