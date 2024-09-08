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
    public partial class AgreementUserTypeRepository : RepositoryBase<AgreementUserType>, IAgreementUserTypeRepository
    {
        public AgreementUserTypeRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
        
    }
    
    public partial interface IAgreementUserTypeRepository : IRepository<AgreementUserType>
    {
    }
}

