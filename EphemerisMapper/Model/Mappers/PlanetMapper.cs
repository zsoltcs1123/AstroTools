using System.Runtime.CompilerServices;
using EphemerisMapper.Model.Enums;
using EphemerisMapper.Model.ZodiacPosition;

namespace EphemerisMapper.Model.Mappers;

public static class PlanetMapper
{
    /*private const decimal VimshottariTotal = 120m;

    private static readonly Dictionary<PlanetEnum, int> PlanetsToVimshottariPeriod =
        Enum.GetValues<PlanetEnum>().ToDictionary(p => p, p => p.ToVimshottariPeriod());
    
    private static int ToVimshottariPeriod(this PlanetEnum planetEnum) => planetEnum switch
    {
        PlanetEnum.Ketu => 7,
        PlanetEnum.Venus => 20,
        PlanetEnum.Sun => 6,
        PlanetEnum.Moon => 10,
        PlanetEnum.Mars => 7,
        PlanetEnum.Rahu => 18,
        PlanetEnum.Jupiter => 16,
        PlanetEnum.Saturn => 19,
        PlanetEnum.Mercury => 17,
        _ => 0
    };

    public static IEnumerable<StarEnum> ToStar(this PlanetEnum planetEnum) => planetEnum switch
    {
        PlanetEnum.Ketu => new[] { StarEnum.Aswini, StarEnum.Magha, StarEnum.Mula },
        PlanetEnum.Venus => new[] { StarEnum.Bharani, StarEnum.PurvaPhalguni, StarEnum.PurvaAshadha },
        PlanetEnum.Sun => new[] { StarEnum.Krittika, StarEnum.UttaraPhalguni, StarEnum.UttaraAshadha },
        PlanetEnum.Moon => new[] { StarEnum.Rohini, StarEnum.Hasta, StarEnum.Sravana },
        PlanetEnum.Mars => new[] { StarEnum.Mrigashira, StarEnum.Chitra, StarEnum.Dhanishta },
        PlanetEnum.Rahu => new[] { StarEnum.Adra, StarEnum.Swati, StarEnum.Shatabhisha },
        PlanetEnum.Jupiter => new[] { StarEnum.Punarvasu, StarEnum.Vishaka, StarEnum.PurvaBhadrapada },
        PlanetEnum.Saturn => new[] { StarEnum.Pushya, StarEnum.Anuradha, StarEnum.UttaraBhadrapada },
        PlanetEnum.Mercury => new[] { StarEnum.Aslesha, StarEnum.Jyestha, StarEnum.Revati },
        _ => Array.Empty<StarEnum>()
    };

    public static DegreeRange ToDegreeRange(this PlanetEnum planetEnum, decimal offset = 0)
    {
        var st = (PlanetsToVimshottariPeriod[planetEnum] / VimshottariTotal) * StarMapper.StarRegion.Dec;
        var start = (PlanetsToVimshottariPeriod
            .Where(ptv => (int)ptv.Key < (int)planetEnum)
            .Select(ptv => ptv.Value)
            .Sum() / VimshottariTotal) * StarMapper.StarRegion.Dec +offset;

        var end = start + (PlanetsToVimshottariPeriod[planetEnum]/VimshottariTotal) * StarMapper.StarRegion.Dec;
        
        return new DegreeRange(
            new Degree(start).RoundToNearestWhole(),
            new Degree(end).RoundToNearestWhole());
    }

    public static decimal ToVimShottari(this PlanetEnum planetEnum) =>
        PlanetsToVimshottariPeriod[planetEnum] / VimshottariTotal;*/
}