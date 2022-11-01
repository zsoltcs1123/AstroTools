using EphemerisMapper.Model.CelestialObjects;
using EphemerisMapper.Model.Enums;
using EphemerisMapper.Model.SubDivisions;
using EphemerisMapper.Model.Units;

namespace EphemerisMapper.Model.Divisions;

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