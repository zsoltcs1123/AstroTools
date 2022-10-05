using SubLordMapper.Model.ZodiacPosition;

namespace SubLordMapper.Model
{
    public record SubLord(int Id, string Sign, string ZodiacLord, string Nakshatra, string StarLord, string Sub, DegreeRange Degrees);

}
