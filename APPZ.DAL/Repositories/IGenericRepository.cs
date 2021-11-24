using APPZ.DAL.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace APPZ.DAL.Repositories
{
    public interface IGenericRepository<TEntity, TId> where TEntity : class, IEntity<TId>
    {
           Task<List<TEntity>> GetAsync(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null
        );

        Task<TEntity> GetByIdAsync(TId id);
        Task InsertAsync(TEntity entity);
        void Update(TEntity entityToUpdate);
        void Delete(TEntity entityToDelete);
        Task Delete(TId id);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
