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
    public partial class vw_UserMenuOptionRoleGridRepository : RepositoryBase<vw_UserMenuOptionRoleGrid>, Ivw_UserMenuOptionRoleGridRepository
    {
        public vw_UserMenuOptionRoleGridRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
        
    }
    
    public partial interface Ivw_UserMenuOptionRoleGridRepository : IRepository<vw_UserMenuOptionRoleGrid>
    {
    }
}

