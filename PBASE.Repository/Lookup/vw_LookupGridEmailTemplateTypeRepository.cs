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
    public partial class vw_LookupGridEmailTemplateTypeRepository : RepositoryBase<vw_LookupGridEmailTemplateType>, Ivw_LookupGridEmailTemplateTypeRepository
    {
        public vw_LookupGridEmailTemplateTypeRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
    }
    
    public partial interface Ivw_LookupGridEmailTemplateTypeRepository : IRepository<vw_LookupGridEmailTemplateType>
    {
    }
}

