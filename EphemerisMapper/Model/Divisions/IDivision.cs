using EphemerisMapper.Model.SubDivisions;
using EphemerisMapper.Model.Units;

namespace EphemerisMapper.Model.Divisions;

public interface IDivision
{
    string Name { get; }
    DegreeRange Region { get; }
    public List<SubDivision> SubDivisions { get; }
}