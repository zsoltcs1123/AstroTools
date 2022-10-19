using EphemerisMapper.Model.Attributes;
using EphemerisMapper.Model.Enums;

namespace EphemerisMapper.Extensions;

public static class StarEnumExtensions
{
    /*public static readonly Degree StarRegion = new(13, 20, 0);

    private static readonly Dictionary<DegreeRange, StarEnum> CuspToStar = GenerateCuspToStar();

    private static readonly Dictionary<StarEnum, PlanetEnum> StarLords = GenerateStarLords();

    private static Dictionary<DegreeRange, StarEnum> GenerateCuspToStar()
    {
        decimal acc = 0;
        return Enum.GetValues<StarEnum>()
            .ToDictionary(
                p => new DegreeRange(new Degree(acc).RoundToNearestWhole(),
                    new Degree(acc += StarRegion.Dec).RoundToNearestWhole()), p => p);
    }

    private static Dictionary<StarEnum, PlanetEnum> GenerateStarLords()
    {
        var ret = new Dictionary<StarEnum, PlanetEnum>();
        var planetToStar = Enum.GetValues<PlanetEnum>().ToDictionary(p => p, p => p.ToStar());

        foreach (var pts in planetToStar)
        {
            foreach (var star in pts.Value)
            {
                ret.Add(star, pts.Key);
            }
        }

        return ret;
    }

    public static StarEnum ToStar(this Degree degree)
    {
        return CuspToStar.First(cts => cts.Key.Contains(degree)).Value;
    }*/

    public static PlanetEnum ToLord(this StarEnum starEnum) => starEnum.Get<TraditionalLordAttribute>().Lord;


}