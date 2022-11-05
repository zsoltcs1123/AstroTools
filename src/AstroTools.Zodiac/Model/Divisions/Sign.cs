using AstroTools.Common.Model.Degree;
using AstroTools.Zodiac.Model.CelestialObjects;
using AstroTools.Zodiac.Model.Enums;
using AstroTools.Zodiac.Model.SubDivisions;

namespace AstroTools.Zodiac.Model.Divisions;

public record Sign(string Name, SignEnum SignEnum, DegreeRange Region, Planet Lord) : IDivision
{
    public List<SubDivision> SubDivisions { get; } = new();
}