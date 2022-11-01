using EphemerisMapper.Model.CelestialObjects;
using EphemerisMapper.Model.SubDivisions;
using EphemerisMapper.Model.Units;

namespace EphemerisMapper.Model.Divisions;

public interface IDivision : IMappable
{
    DegreeRange Region { get; }
    public List<SubDivision> SubDivisions { get; }
}