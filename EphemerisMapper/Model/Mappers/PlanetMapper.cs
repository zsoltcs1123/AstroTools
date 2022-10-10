using System.Runtime.CompilerServices;
using EphemerisMapper.Model.Enums;
using EphemerisMapper.Model.ZodiacPosition;

namespace EphemerisMapper.Model.Mappers;

public static class PlanetMapper
{
    private const decimal VimshottariTotal = 120m;

    private static readonly Dictionary<Planet, int> PlanetsToVimshottariPeriod =
        Enum.GetValues<Planet>().ToDictionary(p => p, p => p.ToVimshottariPeriod());
    
    private static int ToVimshottariPeriod(this Planet planet) => planet switch
    {
        Planet.Ketu => 7,
        Planet.Venus => 20,
        Planet.Sun => 6,
        Planet.Moon => 10,
        Planet.Mars => 7,
        Planet.Rahu => 18,
        Planet.Jupiter => 16,
        Planet.Saturn => 19,
        Planet.Mercury => 17,
        _ => 0
    };

    public static IEnumerable<Star> ToStar(this Planet planet) => planet switch
    {
        Planet.Ketu => new[] { Star.Aswini, Star.Magha, Star.Mula },
        Planet.Venus => new[] { Star.Bharani, Star.PurvaPhalguni, Star.PurvaAshadha },
        Planet.Sun => new[] { Star.Krittika, Star.UttaraPhalguni, Star.UttaraAshadha },
        Planet.Moon => new[] { Star.Rohini, Star.Hasta, Star.Sravana },
        Planet.Mars => new[] { Star.Mrigashira, Star.Chitra, Star.Dhanishta },
        Planet.Rahu => new[] { Star.Adra, Star.Swati, Star.Shatabhisha },
        Planet.Jupiter => new[] { Star.Punarvasu, Star.Vishaka, Star.PurvaBhadrapada },
        Planet.Saturn => new[] { Star.Pushya, Star.Anuradha, Star.UttaraBhadrapada },
        Planet.Mercury => new[] { Star.Aslesha, Star.Jyestha, Star.Revati },
        _ => Array.Empty<Star>()
    };

    public static DegreeRange ToDegreeRange(this Planet planet, decimal offset = 0)
    {
        var st = (PlanetsToVimshottariPeriod[planet] / VimshottariTotal) * StarMapper.StarRegion.Dec;
        var start = (PlanetsToVimshottariPeriod
            .Where(ptv => (int)ptv.Key < (int)planet)
            .Select(ptv => ptv.Value)
            .Sum() / VimshottariTotal) * StarMapper.StarRegion.Dec +offset;

        var end = start + (PlanetsToVimshottariPeriod[planet]/VimshottariTotal) * StarMapper.StarRegion.Dec;
        
        return new DegreeRange(
            new Degree(start).RoundToNearestWhole(),
            new Degree(end).RoundToNearestWhole());
    }

    public static decimal ToVimShottari(this Planet planet) =>
        PlanetsToVimshottariPeriod[planet] / VimshottariTotal;
}