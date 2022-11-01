namespace AstroTools.Common.Factory;

public interface IFactory<out T>
{
    IEnumerable<T> CreateAll();
}