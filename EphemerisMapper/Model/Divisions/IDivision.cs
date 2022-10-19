using EphemerisMapper.Model.ZodiacPosition;

namespace EphemerisMapper.Model.Divisions;

public interface IDivision
{
    string Name { get; }
    DegreeRange Region { get; }
    IEnumerable<SubDivision>? SubDivisions { get; }
}