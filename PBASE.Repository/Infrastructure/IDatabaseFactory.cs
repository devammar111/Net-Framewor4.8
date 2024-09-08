using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PBASE.Repository.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        AppContext Get();
    }
}
