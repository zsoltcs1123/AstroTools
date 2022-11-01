namespace AstroTools.Common.Repository;

public interface IRepository<out T>
{
    IEnumerable<T> GetAll();
}