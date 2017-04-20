using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Framework.Data
{
    public interface IRepository<TEntity> where TEntity : class
    {        
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> where = null);
        //paging
        IEnumerable<TEntity> GetPage(params Expression<Func<TEntity, object>>[] includeProperties);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties);
        //todo - do we have really have to expose this
        DbContext DbContext { get; }
        //todo - provide return type
        void Validate();
    }
}
