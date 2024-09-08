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
    public partial class ApplicationInformationRepository : RepositoryBase<ApplicationInformation>, IApplicationInformationRepository
    {
        public ApplicationInformationRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
        
    }
    
    public partial interface IApplicationInformationRepository : IRepository<ApplicationInformation>
    {
    }
}

