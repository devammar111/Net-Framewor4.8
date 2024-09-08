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
    public partial class InternalReportFieldRepository : RepositoryBase<InternalReportField>, IInternalReportFieldRepository
    {
        public InternalReportFieldRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
    }
    
    public partial interface IInternalReportFieldRepository : IRepository<InternalReportField>
    {
    }
}
