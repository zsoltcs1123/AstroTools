using SubLordMapper.Model.Divisions;
using SubLordMapper.Model.Enums;
using SubLordMapper.Model.ZodiacPosition;

namespace SubLordMapper.Model.Mappers;

public static class StarMapper
{
    public static readonly Degree StarRegion = new(13, 20, 0);

    private static readonly Dictionary<DegreeRange, Star> CuspToStar = GenerateCuspToStar();

    private static readonly Dictionary<Star, Planet> StarLords = GenerateStarLords();
    private static Dictionary<DegreeRange, Star> GenerateCuspToStar()
    {
        decimal acc = 0;
        return Enum.GetValues<Star>().ToDictionary(p => new DegreeRange(new Degree(acc), new Degree(acc += StarRegion.Decimal)), p => p);
    }
    private static Dictionary<Star,Planet> GenerateStarLords()
    {
        var ret = new Dictionary<Star, Planet>();
        var planetToStar = Enum.GetValues<Planet>().ToDictionary(p=> p, p => p.ToStar());

        foreach (var pts in planetToStar)
        {
            foreach (var star in pts.Value)
            {
                ret.Add(star, pts.Key);
            }
        }

        return ret;
    }
    
    public static Planet ToLord(this Star star) => StarLords[star];

    public static Star ToStar(this Degree degree)
    {
        return CuspToStar.First(cts => cts.Key.Contains(degree)).Value;
    }

    public static DegreeRange ToDegreeRange(this Star star)
    {
        return CuspToStar.First(cts => cts.Value == star).Key;
    }

    public static Nakshatra ToNakshatra(this Degree degree)
    {
        return new Nakshatra(degree.ToStar());
    }

}