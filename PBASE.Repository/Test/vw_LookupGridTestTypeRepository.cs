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
    public partial class vw_LookupGridTestTypeRepository : RepositoryBase<vw_LookupGridTestType>, Ivw_LookupGridTestTypeRepository
    {
        public vw_LookupGridTestTypeRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
    }
    
    public partial interface Ivw_LookupGridTestTypeRepository : IRepository<vw_LookupGridTestType>
    {
    }
}

