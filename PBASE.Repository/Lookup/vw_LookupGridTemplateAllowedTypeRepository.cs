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
    public partial class vw_LookupGridTemplateAllowedTypeRepository : RepositoryBase<vw_LookupGridTemplateAllowedType>, Ivw_LookupGridTemplateAllowedTypeRepository
    {
        public vw_LookupGridTemplateAllowedTypeRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
    }
    
    public partial interface Ivw_LookupGridTemplateAllowedTypeRepository : IRepository<vw_LookupGridTemplateAllowedType>
    {
    }
}

