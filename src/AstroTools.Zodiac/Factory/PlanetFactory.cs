using AstroTools.Common.Factory;
using AstroTools.Zodiac.Model.CelestialObjects;
using AstroTools.Zodiac.Model.Divisions;

namespace AstroTools.Zodiac.Factory;

public class PlanetFactory : IFactory<Planet>
{
    #region Generators

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
            { PlanetEnum.Uranus, 0 },
            { PlanetEnum.Neptune, 0 },
            { PlanetEnum.Pluto, 0 }
        };
    }

    private static Dictionary<PlanetEnum, SignEnum[]> GeneratePlanetToMitra()
    {
        return new Dictionary<PlanetEnum, SignEnum[]>
        {
            { PlanetEnum.Ketu, Array.Empty<SignEnum>() },
            { PlanetEnum.Venus, new[] { SignEnum.Gemini, SignEnum.Virgo, SignEnum.Capricorn, SignEnum.Aquarius } },
            {
                PlanetEnum.Sun,
                new[] { SignEnum.Cancer, SignEnum.Aries, SignEnum.Scorpio, SignEnum.Saggitarius, SignEnum.Pisces }
            },
            { PlanetEnum.Moon, new[] { SignEnum.Leo, SignEnum.Gemini, SignEnum.Virgo } },
            { PlanetEnum.Mars, new[] { SignEnum.Leo, SignEnum.Cancer, SignEnum.Saggitarius, SignEnum.Pisces } },
            { PlanetEnum.Rahu, Array.Empty<SignEnum>() },
            { PlanetEnum.Jupiter, new[] { SignEnum.Leo, SignEnum.Cancer, SignEnum.Aries, SignEnum.Scorpio } },
            { PlanetEnum.Saturn, new[] { SignEnum.Gemini, SignEnum.Virgo, SignEnum.Taurus, SignEnum.Libra } },
            { PlanetEnum.Mercury, new[] { SignEnum.Leo, SignEnum.Taurus, SignEnum.Libra } },
            { PlanetEnum.Uranus, Array.Empty<SignEnum>() },
            { PlanetEnum.Neptune, Array.Empty<SignEnum>() },
            { PlanetEnum.Pluto, Array.Empty<SignEnum>() }
        };
    }

    private static Dictionary<PlanetEnum, SignEnum[]> GeneratePlanetToSatru()
    {
        return new Dictionary<PlanetEnum, SignEnum[]>
        {
            { PlanetEnum.Ketu, Array.Empty<SignEnum>() },
            { PlanetEnum.Venus, new[] { SignEnum.Cancer, SignEnum.Leo } },
            { PlanetEnum.Sun, new[] { SignEnum.Taurus, SignEnum.Libra, SignEnum.Capricorn, SignEnum.Aquarius } },
            { PlanetEnum.Moon, Array.Empty<SignEnum>() },
            { PlanetEnum.Mars, new[] { SignEnum.Gemini, SignEnum.Virgo } },
            { PlanetEnum.Rahu, Array.Empty<SignEnum>() },
            { PlanetEnum.Jupiter, new[] { SignEnum.Gemini, SignEnum.Virgo, SignEnum.Taurus, SignEnum.Libra } },
            { PlanetEnum.Saturn, new[] { SignEnum.Leo, SignEnum.Cancer, SignEnum.Aries, SignEnum.Scorpio } },
            { PlanetEnum.Mercury, new[] { SignEnum.Cancer } },
            { PlanetEnum.Uranus, Array.Empty<SignEnum>() },
            { PlanetEnum.Neptune, Array.Empty<SignEnum>() },
            { PlanetEnum.Pluto, Array.Empty<SignEnum>() }
        };
    }

    private static Dictionary<PlanetEnum, SignEnum[]> GeneratePlanetToLord()
    {
        return new Dictionary<PlanetEnum, SignEnum[]>
        {
            { PlanetEnum.Ketu, Array.Empty<SignEnum>() },
            { PlanetEnum.Venus, new[] { SignEnum.Taurus, SignEnum.Libra } },
            { PlanetEnum.Sun, new[] { SignEnum.Leo } },
            { PlanetEnum.Moon, new[] { SignEnum.Cancer } },
            { PlanetEnum.Mars, new[] { SignEnum.Aries, SignEnum.Scorpio } },
            { PlanetEnum.Rahu, Array.Empty<SignEnum>() },
            { PlanetEnum.Jupiter, new[] { SignEnum.Saggitarius, SignEnum.Pisces } },
            { PlanetEnum.Saturn, new[] { SignEnum.Capricorn, SignEnum.Aquarius } },
            { PlanetEnum.Mercury, new[] { SignEnum.Gemini, SignEnum.Virgo } },
            { PlanetEnum.Uranus, new[] { SignEnum.Aquarius } },
            { PlanetEnum.Neptune, new[] { SignEnum.Pisces } },
            { PlanetEnum.Pluto, new[] { SignEnum.Scorpio } }
        };
    }

    private static Dictionary<PlanetEnum, SignEnum[]> GeneratePlanetToExalted()
    {
        return new Dictionary<PlanetEnum, SignEnum[]>
        {
            { PlanetEnum.Ketu, Array.Empty<SignEnum>() },
            { PlanetEnum.Venus, new[] { SignEnum.Pisces } },
            { PlanetEnum.Sun, new[] { SignEnum.Aries } },
            { PlanetEnum.Moon, new[] { SignEnum.Taurus } },
            { PlanetEnum.Mars, new[] { SignEnum.Capricorn } },
            { PlanetEnum.Rahu, Array.Empty<SignEnum>() },
            { PlanetEnum.Jupiter, new[] { SignEnum.Cancer } },
            { PlanetEnum.Saturn, new[] { SignEnum.Libra } },
            { PlanetEnum.Mercury, new[] { SignEnum.Virgo } },
            { PlanetEnum.Uranus, Array.Empty<SignEnum>() },
            { PlanetEnum.Neptune, Array.Empty<SignEnum>() },
            { PlanetEnum.Pluto, Array.Empty<SignEnum>() },
        };
    }

    private static Dictionary<PlanetEnum, SignEnum[]> GeneratePlanetToDebilitated()
    {
        return new Dictionary<PlanetEnum, SignEnum[]>
        {
            { PlanetEnum.Ketu, Array.Empty<SignEnum>() },
            { PlanetEnum.Venus, new[] { SignEnum.Virgo } },
            { PlanetEnum.Sun, new[] { SignEnum.Libra } },
            { PlanetEnum.Moon, new[] { SignEnum.Scorpio } },
            { PlanetEnum.Mars, new[] { SignEnum.Cancer } },
            { PlanetEnum.Rahu, Array.Empty<SignEnum>() },
            { PlanetEnum.Jupiter, new[] { SignEnum.Capricorn } },
            { PlanetEnum.Saturn, new[] { SignEnum.Aries } },
            { PlanetEnum.Mercury, new[] { SignEnum.Pisces } },
            { PlanetEnum.Uranus, Array.Empty<SignEnum>() },
            { PlanetEnum.Neptune, Array.Empty<SignEnum>() },
            { PlanetEnum.Pluto, Array.Empty<SignEnum>() },
        };
    }

    private static Dictionary<PlanetEnum, SignEnum[]> GeneratePlanetToSama()
    {
        return AllPlanets.ToDictionary(p => p,
            p => AllSigns.Except(PlanetToMitra[p].Except(PlanetToSatru[p].Except(PlanetToLord[p]))).ToArray());
    }

    #endregion

    private static readonly PlanetEnum[] AllPlanets = Enum.GetValues<PlanetEnum>();
    private static readonly SignEnum[] AllSigns = Enum.GetValues<SignEnum>();

    private static readonly Dictionary<PlanetEnum, int> PlanetsToVimshottariPeriod =
        GeneratePlanetsToVimshottariPeriod();

    private static readonly Dictionary<PlanetEnum, SignEnum[]> PlanetToMitra = GeneratePlanetToMitra();
    private static readonly Dictionary<PlanetEnum, SignEnum[]> PlanetToSatru = GeneratePlanetToSatru();
    private static readonly Dictionary<PlanetEnum, SignEnum[]> PlanetToLord = GeneratePlanetToLord();
    private static readonly Dictionary<PlanetEnum, SignEnum[]> PlanetToExalted = GeneratePlanetToExalted();
    private static readonly Dictionary<PlanetEnum, SignEnum[]> PlanetToDebilitated = GeneratePlanetToDebilitated();
    private static readonly Dictionary<PlanetEnum, SignEnum[]> PlanetToSama = GeneratePlanetToSama();

    public IEnumerable<Planet> CreateAll()
    {
        return AllPlanets
            .Select(pe => new Planet(
                pe,
                PlanetsToVimshottariPeriod[pe],
                PlanetToLord[pe],
                PlanetToMitra[pe],
                PlanetToSatru[pe],
                PlanetToSama[pe],
                PlanetToExalted[pe],
                PlanetToDebilitated[pe]));
    }
}