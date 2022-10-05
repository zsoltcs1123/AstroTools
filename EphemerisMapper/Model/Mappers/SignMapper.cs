using EphemerisMapper.Model.Enums;
using EphemerisMapper.Model.ZodiacPosition;

namespace EphemerisMapper.Model.Mappers;

public static class SignMapper
{
    private static readonly Degree StarRegion = new Degree(0, 13, 20, 0);

    private static readonly Dictionary<DegreeRange, Sign> CuspToSign = GenerateCuspToSign();

    private static Dictionary<DegreeRange,Sign> GenerateCuspToSign()
    {
        decimal acc = 0;
        return Enum.GetValues<Sign>().ToDictionary(p => new DegreeRange(new Degree(acc), new Degree(acc += StarRegion.Dec)), p => p);
    }
    
    public static Sign ToSign(this Degree degree)
    {
        return CuspToSign.First(cts => cts.Key.Contains(degree)).Value;
    }

    public static Planet ToLord(this Sign sign) => sign switch
    {
        _ => Planet.Jupiter
    };
}