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
    public partial class InternalGridSettingDefaultRepository : RepositoryBase<InternalGridSettingDefault>, IInternalGridSettingDefaultRepository
    {
        public InternalGridSettingDefaultRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
    }
    
    public partial interface IInternalGridSettingDefaultRepository : IRepository<InternalGridSettingDefault>
    {
    }
}

