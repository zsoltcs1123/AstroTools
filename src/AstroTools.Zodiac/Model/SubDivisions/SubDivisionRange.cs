using AstroTools.CelestialObjects.Model;
using AstroTools.Common.Model.Degree;

namespace AstroTools.Zodiac.Model.SubDivisions;

public record SubDivisionRange(DegreeRange Range, Planet Lord, SubDivision? SubDivision = null);