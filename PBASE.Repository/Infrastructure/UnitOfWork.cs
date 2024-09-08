using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBASE.Repository.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDatabaseFactory databaseFactory;
        private AppContext dataContext;

        public UnitOfWork(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }

        protected AppContext DataContext
        {
            get { return dataContext ?? (dataContext = databaseFactory.Get()); }
        }

        public void Commit(int currentUserId)
        {
            DataContext.Commit(currentUserId);
        }

        public async Task<int> CommitAsync(int currentUserId)
        {
            return await DataContext.CommitAsync(currentUserId);
        }
    }
}
