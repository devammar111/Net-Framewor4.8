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
    public partial class vw_EmailTemplateGridRepository : RepositoryBase<vw_EmailTemplateGrid>, Ivw_EmailTemplateGridRepository
    {
        public vw_EmailTemplateGridRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
        
    }
    
    public partial interface Ivw_EmailTemplateGridRepository : IRepository<vw_EmailTemplateGrid>
    {
    }
}

