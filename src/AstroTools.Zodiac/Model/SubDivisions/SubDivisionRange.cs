using AstroTools.Common.Model.Degree;
using AstroTools.Zodiac.Model.CelestialObjects;

namespace AstroTools.Zodiac.Model.SubDivisions;

public record SubDivisionRange(DegreeRange Range, Planet Lord, SubDivision? SubDivision = null);