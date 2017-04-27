using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;
using Framework.Data.ExceptionHandling;
using Framework.Data.Validation;

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
            if(where == null)
                return await _dbSet.CountAsync();
            else
                return await _dbSet.CountAsync(where);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            SaveChanges(entity);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> query, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            foreach (var prop in includeProperties)            
                _dbSet.Include(prop);
            
            return _dbSet.Where(query).ToList();
        }

        public IEnumerable<TEntity> GetPage(int pageIndex = 0, int pageSize = 30, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            if (pageSize > 30)
                pageSize = 30;
                
            foreach (var prop in includeProperties)
                _dbSet.Include(prop);

            return _dbSet.Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }

        public TEntity Insert(TEntity entity)
        {
            entity = _dbSet.Add(entity).Entity;
            SaveChanges(entity);
            return entity;            
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
            SaveChanges(entity);
        }

        private void Validate(TEntity entity)
        {
            ICollection<ValidationResult> results = new Collection<ValidationResult>();
            ValidationContext context = new ValidationContext(entity);
            bool isValid = Validator.TryValidateObject(entity, context, results, true);
            if (!isValid)
                throw new ValidationResultException("Data validation failed", results);
        }

        private void SaveChanges(TEntity entity)
        {
            Validate(entity);
            try
            {
                _dbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new DbException("An error occured while saving your changes", ex);
            }
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
