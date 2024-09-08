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
    public partial class vw_SystemAlertGridRepository : RepositoryBase<vw_SystemAlertGrid>, Ivw_SystemAlertGridRepository
    {
        public vw_SystemAlertGridRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
        
    }
    
    public partial interface Ivw_SystemAlertGridRepository : IRepository<vw_SystemAlertGrid>
    {
    }
}

