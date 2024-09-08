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
    public partial class vw_LookupGridAlertTypeRepository : RepositoryBase<vw_LookupGridAlertType>, Ivw_LookupGridAlertTypeRepository
    {
        public vw_LookupGridAlertTypeRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
    }
    
    public partial interface Ivw_LookupGridAlertTypeRepository : IRepository<vw_LookupGridAlertType>
    {
    }
}

