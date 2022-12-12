using AstroTools.Common.Model.Degree;
using AstroTools.Zodiac.Model.CelestialObjects;

namespace AstroTools.Zodiac.Model.Divisions;

public interface IDivision : ICelestialObject
{
    DegreeRange Region { get; }
    Planet Lord { get; }
    public Dictionary<string, SubDivision> SubDivisions { get; set; }
}