using CCMERP.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CCMERP.Service.Identity.Repositories
{
    public class IdentityEntityBaseRepository<T> : IIdentityEntityBaseRepository<T>
              where T : class, new()
    {
        private IdentityContext _context;
        private DbSet<T> entities;
        #region Properties
        public IdentityEntityBaseRepository(IdentityContext context)
        {
            _context = context;
            entities = _context.Set<T>();
        }

        public virtual async Task<int> AddAsynAsync(T t)
        {
            try
            {

                if (t == null)
                {
                    throw new ArgumentNullException("entity");
                }
                entities.Add(t);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

            }
            return await _context.SaveChangesAsync();

        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }

        public virtual async Task<int> DeleteAsyn(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                entities.Remove(entity);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {

            }
            return await _context.SaveChangesAsync();
        }

        public virtual async Task<int> DeleteWhereAsync(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> entities = _context.Set<T>().Where(predicate);

            foreach (var entity in entities)
            {
                _context.Entry<T>(entity).State = EntityState.Deleted;
            }
            return await _context.SaveChangesAsync();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<List<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await _context.Set<T>().Where(match).ToListAsync();
        }

        public List<T> FindAll(Expression<Func<T, bool>> match)
        {
            return _context.Set<T>().Where(match).ToList();
        }

        public T Find(Expression<Func<T, bool>> match)
        {
            return _context.Set<T>().SingleOrDefault(match);
        }


        public async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(match);
        }

        public virtual async Task<List<T>> FindByAsyn(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public virtual async Task<List<T>> GetAllAsyn()
        {

            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<int> UpdateAsyn(T t, object key)
        {
            var check = 0;
            try
            {
                if (t == null)
                {
                    return await Task.FromResult<int>(check);
                }

                T exist = await _context.Set<T>().FindAsync(key);
                if (exist != null)
                {
                    _context.Update<T>(exist);

                    _context.SaveChanges();

                }
            }
            catch (Exception ex)
            {

            }
            return await Task.FromResult<int>(check);
        }

        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }
        #endregion

    }
}
