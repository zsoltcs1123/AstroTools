using EphemerisMapper.Model.ZodiacPosition;
using EphemerisMapper.Model.Mappers;

namespace EphemerisMapper.Model.Divisions;

public record SubDivision(string Name, IEnumerable<SubDivisionRange> Ranges);