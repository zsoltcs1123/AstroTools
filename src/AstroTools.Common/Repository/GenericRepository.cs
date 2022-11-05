using System.Data.Entity;
using System.Linq.Expressions;

namespace AstroTools.Common.Repository;

public class GenericRepository<TEntity> : IRepository<TEntity>
{
    private readonly IEnumerable<TEntity> _dataSet;

    public GenericRepository(IEnumerable<TEntity> dataSet)
    {
        _dataSet = dataSet;
    }

    public IEnumerable<TEntity> GetAll()
    {
        return _dataSet;
    }

    public virtual IEnumerable<TEntity> Get(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "")
    {
        IQueryable<TEntity> query = _dataSet.AsQueryable();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        query = includeProperties
            .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
            .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

        return orderBy != null ? orderBy(query).ToList() : query.ToList();
    }
}