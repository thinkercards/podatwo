using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CCMERP.Service.Identity.Repositories
{
    public interface IIdentityEntityBaseRepository<T> where T : class, new()
    {
        Task<int> AddAsynAsync(T t);
        Task<int> CountAsync();
        Task<int> DeleteAsyn(T entity);
        Task<int> DeleteWhereAsync(Expression<Func<T, bool>> predicate);
        void Dispose();
        Task<T> FindAsync(Expression<Func<T, bool>> match);
        T Find(Expression<Func<T, bool>> match);
        Task<List<T>> FindAllAsync(Expression<Func<T, bool>> match);
        List<T> FindAll(Expression<Func<T, bool>> match);

        Task<List<T>> FindByAsyn(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetAllAsyn();
        Task<int> UpdateAsyn(T t, object key);
        T GetSingle(Expression<Func<T, bool>> predicate);
    }
}
