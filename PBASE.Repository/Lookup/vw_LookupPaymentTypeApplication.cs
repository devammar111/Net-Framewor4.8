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
    public partial class vw_LookupPaymentTypeApplicationRepository : RepositoryBase<vw_LookupPaymentTypeApplication>, Ivw_LookupPaymentTypeApplicationRepository
    {
        public vw_LookupPaymentTypeApplicationRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            //
        }

    }

    public partial interface Ivw_LookupPaymentTypeApplicationRepository : IRepository<vw_LookupPaymentTypeApplication>
    {
    }
}

