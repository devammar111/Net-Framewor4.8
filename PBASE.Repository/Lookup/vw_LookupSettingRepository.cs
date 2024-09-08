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
    public partial class vw_LookupSettingRepository : RepositoryBase<vw_LookupSetting>, Ivw_LookupSettingRepository
    {
        public vw_LookupSettingRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            //
        }
    }
    
    public partial interface Ivw_LookupSettingRepository : IRepository<vw_LookupSetting>
    {
    }
}

