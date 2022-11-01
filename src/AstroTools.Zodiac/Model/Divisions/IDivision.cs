using AstroTools.Common.Model;
using AstroTools.Common.Model.Degree;
using AstroTools.Zodiac.Model.SubDivisions;

namespace AstroTools.Zodiac.Model.Divisions;

public interface IDivision : IMappable
{
    DegreeRange Region { get; }
    public List<SubDivision> SubDivisions { get; }
}