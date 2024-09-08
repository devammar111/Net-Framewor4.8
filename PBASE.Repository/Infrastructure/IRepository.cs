using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Probase.GridHelper;
using System.Threading.Tasks;

namespace PBASE.Repository.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Update(T entity, object model);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);
        T SelectById(long Id);
        T SelectById(string Id);
        T Select(Expression<Func<T, bool>> where);
        List<T> SelectByGridSetting(GridSetting gridSetting);
        IEnumerable<T> SelectAll();
        IEnumerable<T> SelectMany(Expression<Func<T, bool>> where);

        #region
        Task<T> SelectByIdAsync(long id);
        Task<IEnumerable<T>> SelectAllAsync();
        Task<IEnumerable<T>> SelectManyAsync(Expression<Func<T, bool>> where);
        Task<T> SelectAsync(Expression<Func<T, bool>> where);
        Task<int> TotalAsync(Expression<Func<T, bool>> where);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> where);
        Task<List<T>> SelectByGridSettingAsync(GridSetting gridSetting);

        #endregion
    }
}
