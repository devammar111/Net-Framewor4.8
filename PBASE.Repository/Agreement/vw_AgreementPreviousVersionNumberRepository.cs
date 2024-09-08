using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity.Core.Objects;
using Probase.GridHelper;
using PBASE.Repository.Infrastructure;
using PBASE.Entity;
using System.Data.Entity;
namespace PBASE.Repository
{
    public partial class vw_AgreementPreviousVersionNumberRepository : RepositoryBase<vw_AgreementPreviousVersionNumber>, Ivw_AgreementPreviousVersionNumberRepository
    {
        public vw_AgreementPreviousVersionNumberRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }

   }
    
    public partial interface Ivw_AgreementPreviousVersionNumberRepository : IRepository<vw_AgreementPreviousVersionNumber>
    {
    }
}

