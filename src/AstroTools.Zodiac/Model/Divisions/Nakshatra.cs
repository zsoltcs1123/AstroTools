using AstroTools.Common.Model.Degree;
using AstroTools.Zodiac.Model.CelestialObjects;

namespace AstroTools.Zodiac.Model.Divisions;

public record Nakshatra(StarEnum SignEnum, DegreeRange Region, Planet Lord) : IDivision
{
    public string Name => SignEnum.ToString();
    public Dictionary<string, SubDivision> SubDivisions { get; set; } = new();
}