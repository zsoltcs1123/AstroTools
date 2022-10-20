using EphemerisMapper.Model.Divisions;
using EphemerisMapper.Model.Units;
using EphemerisMapper.Service.Builder.Division;

namespace EphemerisMapper.Service.Repository.Division;

public class DivisionRepository<T> : IDivisionRepository<T> where T : IDivision
{
    private readonly Dictionary<DegreeRange, T> _degreeToDivisionMap;

    public DivisionRepository(IDivisionBuilder<T> divisionBuilder)
    {
        _degreeToDivisionMap = divisionBuilder.BuildDivisions().ToDictionary(s => s.Region, s => s);
    }

    public T Get(Degree degree) => _degreeToDivisionMap.First(dts => dts.Key.Contains(degree)).Value;

    public IEnumerable<T> GetAll()
    {
        return _degreeToDivisionMap.Values;
    }
}