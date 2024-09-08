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
    public partial class vw_SystemAlertIsClosedRepository : RepositoryBase<vw_SystemAlertIsClosed>, Ivw_SystemAlertIsClosedRepository
    {
        public vw_SystemAlertIsClosedRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
        
    }
    
    public partial interface Ivw_SystemAlertIsClosedRepository : IRepository<vw_SystemAlertIsClosed>
    {
    }
}

