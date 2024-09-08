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
    public partial class vw_UserGroupGridRepository : RepositoryBase<vw_UserGroupGrid>, Ivw_UserGroupGridRepository
    {
        public vw_UserGroupGridRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
        
    }
    
    public partial interface Ivw_UserGroupGridRepository : IRepository<vw_UserGroupGrid>
    {
    }
}

