using AstroTools.Common.Model;
using AstroTools.Common.Model.Degree;

namespace AstroTools.Zodiac.Model.Divisions;

public record SubDivisionRange(DegreeRange Range, IMappable Mapped, SubDivision? SubDivision = null);