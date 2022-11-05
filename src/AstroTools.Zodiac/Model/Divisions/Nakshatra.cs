using AstroTools.Common.Model.Degree;
using AstroTools.Zodiac.Model.CelestialObjects;
using AstroTools.Zodiac.Model.Enums;
using AstroTools.Zodiac.Model.SubDivisions;

namespace AstroTools.Zodiac.Model.Divisions;

public record Nakshatra : IDivision
{
    public string Name { get; }
    public DegreeRange Region { get; }
    public Planet Lord { get; }
    public List<SubDivision> SubDivisions { get; }

    public Nakshatra(StarEnum starEnum, DegreeRange region, Planet lord)
    {
        Name = starEnum.ToString();
        Region = region;
        Lord = lord;
        SubDivisions = new List<SubDivision>();
    }
}