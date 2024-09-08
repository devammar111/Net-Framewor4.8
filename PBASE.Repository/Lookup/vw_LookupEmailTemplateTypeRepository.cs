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
    public partial class vw_LookupEmailTemplateTypeRepository : RepositoryBase<vw_LookupEmailTemplateType>, Ivw_LookupEmailTemplateTypeRepository
    {
        public vw_LookupEmailTemplateTypeRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
    }
    
    public partial interface Ivw_LookupEmailTemplateTypeRepository : IRepository<vw_LookupEmailTemplateType>
    {
    }
}

