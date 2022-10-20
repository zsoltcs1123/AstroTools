using EphemerisMapper.Model.CelestialObjects;
using EphemerisMapper.Model.SubDivisions;
using EphemerisMapper.Model.Units;

namespace EphemerisMapper.Model.Divisions;

public record Sign(string Name, DegreeRange Region, Planet Lord) : IDivision
{
    public List<SubDivision> SubDivisions { get; } = new();
}