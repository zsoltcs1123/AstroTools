using EphemerisMapper.Model.Units;

namespace EphemerisMapper.Service.Builder.Cusp;

public interface ICuspGenerator<T> where T : Enum
{
    Dictionary<T, DegreeRange> GenerateCusps(Degree territory);
}