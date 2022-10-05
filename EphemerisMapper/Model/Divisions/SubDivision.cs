using EphemerisMapper.Model.ZodiacPosition;
using EphemerisMapper.Model.Enums;

namespace EphemerisMapper.Model.Divisions;

public record SubDivision<T>(string Name, int Level, Dictionary<DegreeRange, T> Ranges);