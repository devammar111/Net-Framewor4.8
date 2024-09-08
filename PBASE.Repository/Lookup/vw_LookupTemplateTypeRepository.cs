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
    public partial class vw_LookupTemplateTypeRepository : RepositoryBase<vw_LookupTemplateType>, Ivw_LookupTemplateTypeRepository
    {
        public vw_LookupTemplateTypeRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
    }
    
    public partial interface Ivw_LookupTemplateTypeRepository : IRepository<vw_LookupTemplateType>
    {
    }
}

