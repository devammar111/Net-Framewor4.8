using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity.Core.Objects;
using Probase.GridHelper;
using QCCMS.Repository.Infrastructure;
using QCCMS.Entity;
namespace QCCMS.Repository
{
    public partial class vw_DashboardSubmissionRepository : RepositoryBase<vw_DashboardSubmission>, Ivw_DashboardSubmissionRepository
    {
        public vw_DashboardSubmissionRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            //
        }

    }

    public partial interface Ivw_DashboardSubmissionRepository : IRepository<vw_DashboardSubmission>
    {
    }
}

