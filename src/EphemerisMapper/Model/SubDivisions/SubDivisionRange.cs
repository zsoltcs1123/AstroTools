using EphemerisMapper.Model.CelestialObjects;
using EphemerisMapper.Model.Units;

namespace EphemerisMapper.Model.SubDivisions;

public record SubDivisionRange(DegreeRange Range, Planet Lord, SubDivision? SubDivision = null);