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
    public partial class vw_UserMenuOptionRepository : RepositoryBase<vw_UserMenuOption>, Ivw_UserMenuOptionRepository
    {
        public vw_UserMenuOptionRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
        
    }
    
    public partial interface Ivw_UserMenuOptionRepository : IRepository<vw_UserMenuOption>
    {
    }
}

