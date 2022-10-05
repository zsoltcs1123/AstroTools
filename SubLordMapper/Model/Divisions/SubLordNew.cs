using SubLordMapper.Model.Enums;
using SubLordMapper.Model.ZodiacPosition;

namespace SubLordMapper.Model.Divisions;

public record SubLordNew(int Id, Sign Sign, Planet SignLord, Star Star, Planet StarLord, Planet Sub, DegreeRange Degrees);

public record SubSubLord(int Id, Sign Sign, Planet ZodiacLord, Star Star, Planet StarLord, Planet Sub, Planet SubSub, DegreeRange Degrees);
