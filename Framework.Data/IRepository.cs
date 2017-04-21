using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Framework.Data
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {        
        TEntity Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> where = null);
        //paging
        IEnumerable<TEntity> GetPage(int pageIndex = 0, int pageSize = 30, params Expression<Func<TEntity, object>>[] includeProperties);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> query, params Expression<Func<TEntity, object>>[] includeProperties);        
        //todo - provide return type
        void Validate();
    }
}
