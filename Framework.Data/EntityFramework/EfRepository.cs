using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Framework.Data.EntityFramework
{
    public sealed class EfRepository<TEntity> : IRepository<TEntity>, IDisposable where TEntity : class
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;
        public EfRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }
        
        public async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> where = null)
        {
            return await _dbSet.CountAsync(where);            
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            SaveChanges();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> query, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            foreach (var prop in includeProperties)            
                _dbSet.Include(prop);
            
            return _dbSet.Where(query).ToList();
        }

        public IEnumerable<TEntity> GetPage(int pageIndex = 0, int pageSize = 30, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            foreach (var prop in includeProperties)
                _dbSet.Include(prop);

            return _dbSet.Skip(pageIndex * pageSize).Take(30).ToList();
        }

        public TEntity Insert(TEntity entity)
        {
            entity = _dbSet.Add(entity).Entity;
            SaveChanges();
            return entity;            
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
            SaveChanges();
        }

        public void Validate()
        {
            throw new NotImplementedException();
        }

        private void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _dbContext.Dispose();                    
                }                

                disposedValue = true;
            }
        }        

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);            
        }
        #endregion
    }
}
