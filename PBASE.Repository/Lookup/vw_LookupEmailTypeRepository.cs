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
    public partial class vw_LookupEmailTypeRepository : RepositoryBase<vw_LookupEmailType>, Ivw_LookupEmailTypeRepository
    {
        public vw_LookupEmailTypeRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
    }
    
    public partial interface Ivw_LookupEmailTypeRepository : IRepository<vw_LookupEmailType>
    {
    }
}

