using EphemerisMapper.Model.ZodiacPosition;

namespace EphemerisMapper.Service.Divisions.Cusps;

public interface ICuspGenerator<T> where T : Enum
{
    Dictionary<T, DegreeRange> GenerateCusps(Degree territory);
}