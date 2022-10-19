using EphemerisMapper.Model.CelestialObjects;
using EphemerisMapper.Model.Enums;

namespace EphemerisMapper.Service.Builder;

public class PlanetBuilder : ICelestialObjectBuilder<Planet>
{
    private const decimal VimshottariTotal = 120m;

    private static readonly Dictionary<PlanetEnum, int> PlanetsToVimshottariPeriod =
        GeneratePlanetsToVimshottariPeriod();

    private static Dictionary<PlanetEnum, int> GeneratePlanetsToVimshottariPeriod()
    {
        return new Dictionary<PlanetEnum, int>
        {
            { PlanetEnum.Ketu, 7 },
            { PlanetEnum.Venus, 20 },
            { PlanetEnum.Sun, 6 },
            { PlanetEnum.Moon, 10 },
            { PlanetEnum.Mars, 7 },
            { PlanetEnum.Rahu, 18 },
            { PlanetEnum.Jupiter, 16 },
            { PlanetEnum.Saturn, 19 },
            { PlanetEnum.Mercury, 17 },
        };
    }

    public IEnumerable<Planet> BuildCelestialObjects()
    {
        return Enum.GetValues<PlanetEnum>()
            .Select(pe => new Planet(pe, PlanetsToVimshottariPeriod[pe]));
    }
}