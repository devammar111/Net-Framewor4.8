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
    public partial class vw_UserAgreementFormRepository : RepositoryBase<vw_UserAgreementForm>, Ivw_UserAgreementFormRepository
    {
        public vw_UserAgreementFormRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
        
    }
    
    public partial interface Ivw_UserAgreementFormRepository : IRepository<vw_UserAgreementForm>
    {
    }
}

