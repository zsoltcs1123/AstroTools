using AstroTools.CelestialObjects.Model;
using AstroTools.Common.Model.Degree;
using AstroTools.Zodiac.Model.SubDivisions;

namespace AstroTools.Zodiac.Model.Divisions;

public record Sign(string Name, DegreeRange Region, Planet Lord) : IDivision
{
    public List<SubDivision> SubDivisions { get; } = new();
}