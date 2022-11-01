using AstroTools.Common.Factory;
using AstroTools.Common.Model.Degree;
using AstroTools.Zodiac.Model.Divisions;

namespace AstroTools.Zodiac.Service.Repository;

public class DivisionRepository<T> : IDivisionRepository<T> where T : IDivision
{
    private readonly Dictionary<DegreeRange, T> _degreeToDivisionMap;

    public DivisionRepository(IFactory<T> divisionBuilder)
    {
        _degreeToDivisionMap = divisionBuilder.CreateAll().ToDictionary(s => s.Region, s => s);
    }

    public T Get(Degree degree) => _degreeToDivisionMap.First(dts => dts.Key.Contains(degree)).Value;

    public IEnumerable<T> GetAll()
    {
        return _degreeToDivisionMap.Values;
    }
}