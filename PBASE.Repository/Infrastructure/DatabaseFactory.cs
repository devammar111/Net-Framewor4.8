using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PBASE.Repository.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private AppContext dataContext;
        public AppContext Get()
        {
            return dataContext ?? (dataContext = new AppContext());
        }
        protected override void DisposeCore()
        {
            if (dataContext != null)
                dataContext.Dispose();
        }
    }
}
