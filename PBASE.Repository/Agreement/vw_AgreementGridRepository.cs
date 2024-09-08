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
    public partial class vw_AgreementGridRepository : RepositoryBase<vw_AgreementGrid>, Ivw_AgreementGridRepository
    {
        public vw_AgreementGridRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
        
    }
    
    public partial interface Ivw_AgreementGridRepository : IRepository<vw_AgreementGrid>
    {
    }
}

