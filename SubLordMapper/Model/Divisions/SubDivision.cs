using SubLordMapper.Model.Enums;
using SubLordMapper.Model.ZodiacPosition;

namespace SubLordMapper.Model.Divisions;

public record SubDivision(string Name, int Level, Dictionary<DegreeRange, Planet> Ranges);