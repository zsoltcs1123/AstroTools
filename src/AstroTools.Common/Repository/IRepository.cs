using System.Linq.Expressions;

namespace AstroTools.Common.Repository;

public interface IRepository<TEntity>
{
    IEnumerable<TEntity> GetAll();

    IEnumerable<TEntity> Get(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "");
}