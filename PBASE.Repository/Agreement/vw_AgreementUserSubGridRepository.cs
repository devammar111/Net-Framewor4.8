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
    public partial class vw_AgreementUserSubGridRepository : RepositoryBase<vw_AgreementUserSubGrid>, Ivw_AgreementUserSubGridRepository
    {
        public vw_AgreementUserSubGridRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
        
    }
    
    public partial interface Ivw_AgreementUserSubGridRepository : IRepository<vw_AgreementUserSubGrid>
    {
    }
}

