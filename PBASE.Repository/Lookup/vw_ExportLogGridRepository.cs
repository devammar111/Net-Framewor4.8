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
    public partial class vw_ExportLogGridRepository : RepositoryBase<vw_ExportLogGrid>, Ivw_ExportLogGridRepository
    {
        public vw_ExportLogGridRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
    }
    
    public partial interface Ivw_ExportLogGridRepository : IRepository<vw_ExportLogGrid>
    {
    }
}

