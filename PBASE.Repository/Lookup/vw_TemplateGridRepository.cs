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
    public partial class vw_TemplateGridRepository : RepositoryBase<vw_TemplateGrid>, Ivw_TemplateGridRepository
    {
        public vw_TemplateGridRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
    }
    
    public partial interface Ivw_TemplateGridRepository : IRepository<vw_TemplateGrid>
    {
    }
}

