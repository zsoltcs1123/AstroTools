using EphemerisMapper.Model.CelestialObjects;
using EphemerisMapper.Model.ZodiacPosition;

namespace EphemerisMapper.Model.Divisions;

public record SubDivisionRange(DegreeRange Range, Planet Lord, SubDivision? SubDivision = null);