using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBASE.Repository.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit(int currentUserId);
        Task<int> CommitAsync(int currentUserId);
    }
}
