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
    public partial class vw_LookupAlertTypeRepository : RepositoryBase<vw_LookupAlertType>, Ivw_LookupAlertTypeRepository
    {
        public vw_LookupAlertTypeRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
    }
    
    public partial interface Ivw_LookupAlertTypeRepository : IRepository<vw_LookupAlertType>
    {
    }
}

