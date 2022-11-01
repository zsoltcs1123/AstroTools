namespace AstroTools.Common.Factory;

public interface IParameterizedFactory<out T, in TParameter>
{
    IEnumerable<T> CreateAll(TParameter parameter);
}