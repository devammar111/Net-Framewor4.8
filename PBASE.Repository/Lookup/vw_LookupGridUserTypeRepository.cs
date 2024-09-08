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
    public partial class vw_LookupGridUserTypeRepository : RepositoryBase<vw_LookupGridUserType>, Ivw_LookupGridUserTypeRepository
    {
        public vw_LookupGridUserTypeRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
    }
    
    public partial interface Ivw_LookupGridUserTypeRepository : IRepository<vw_LookupGridUserType>
    {
    }
}

