using EphemerisMapper.Model.CelestialObjects;
using EphemerisMapper.Model.ZodiacPosition;

namespace EphemerisMapper.Model.Divisions;

public record Sign(string Name, DegreeRange Region, Planet Lord, IEnumerable<SubDivision>? SubDivisions = null) : IDivision{
    
}