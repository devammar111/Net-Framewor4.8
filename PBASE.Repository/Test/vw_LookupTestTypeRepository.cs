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
    public partial class vw_LookupTestTypeRepository : RepositoryBase<vw_LookupTestType>, Ivw_LookupTestTypeRepository
    {
        public vw_LookupTestTypeRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
    }
    
    public partial interface Ivw_LookupTestTypeRepository : IRepository<vw_LookupTestType>
    {
    }
}

