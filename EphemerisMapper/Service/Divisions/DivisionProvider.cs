using EphemerisMapper.Model.Divisions;
using EphemerisMapper.Model.ZodiacPosition;
using EphemerisMapper.Service.Builder;

namespace EphemerisMapper.Service.Divisions;

public class DivisionProvider<T> : IDivisionProvider<T> where T : IDivision
{
    private readonly Dictionary<DegreeRange, T> _degreeToSignMap;
    
    public DivisionProvider(IDivisionBuilder<T> signBuilder)
    {
        _degreeToSignMap = signBuilder.BuildDivisions().ToDictionary(s => s.Region, s => s);
    }

    public T Get(Degree degree) => _degreeToSignMap.First(dts => dts.Key.Contains(degree)).Value;
}