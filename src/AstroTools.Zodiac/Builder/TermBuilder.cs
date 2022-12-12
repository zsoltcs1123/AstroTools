using AstroTools.Common.Model.Degree;
using AstroTools.Common.Repository;
using AstroTools.Zodiac.Model.CelestialObjects;
using AstroTools.Zodiac.Model.Divisions;

namespace AstroTools.Zodiac.Builder;

public class TermBuilder
{
    #region Terms definitions

    private static readonly Dictionary<SignEnum, IEnumerable<(PlanetEnum Planet, uint Range)>> Terms = new()
    {
        {
            SignEnum.Aries, new (PlanetEnum, uint)[]
            {
                (PlanetEnum.Jupiter, 6),
                (PlanetEnum.Venus, 6),
                (PlanetEnum.Mercury, 8),
                (PlanetEnum.Mars, 5),
                (PlanetEnum.Saturn, 5)
            }
        },
        {
            SignEnum.Taurus, new (PlanetEnum, uint)[]
            {
                (PlanetEnum.Venus, 8),
                (PlanetEnum.Mercury, 6),
                (PlanetEnum.Jupiter, 8),
                (PlanetEnum.Saturn, 5),
                (PlanetEnum.Mars, 3)
            }
        },
        {
            SignEnum.Gemini, new (PlanetEnum, uint)[]
            {
                (PlanetEnum.Mercury, 6),
                (PlanetEnum.Jupiter, 6),
                (PlanetEnum.Venus, 5),
                (PlanetEnum.Mars, 7),
                (PlanetEnum.Saturn, 6)
            }
        },
        {
            SignEnum.Cancer, new (PlanetEnum, uint)[]
            {
                (PlanetEnum.Mars, 7),
                (PlanetEnum.Venus, 6),
                (PlanetEnum.Mercury, 6),
                (PlanetEnum.Jupiter, 7),
                (PlanetEnum.Saturn, 4)
            }
        },
        {
            SignEnum.Leo, new (PlanetEnum, uint)[]
            {
                (PlanetEnum.Jupiter, 6),
                (PlanetEnum.Venus, 5),
                (PlanetEnum.Saturn, 7),
                (PlanetEnum.Mercury, 6),
                (PlanetEnum.Mars, 6)
            }
        },
        {
            SignEnum.Virgo, new (PlanetEnum, uint)[]
            {
                (PlanetEnum.Mercury, 7),
                (PlanetEnum.Venus, 10),
                (PlanetEnum.Jupiter, 4),
                (PlanetEnum.Mars, 7),
                (PlanetEnum.Saturn, 2)
            }
        },
        {
            SignEnum.Libra, new (PlanetEnum, uint)[]
            {
                (PlanetEnum.Saturn, 6),
                (PlanetEnum.Mercury, 5),
                (PlanetEnum.Jupiter, 7),
                (PlanetEnum.Venus, 6),
                (PlanetEnum.Mars, 6)
            }
        },
        {
            SignEnum.Scorpio, new (PlanetEnum, uint)[]
            {
                (PlanetEnum.Mars, 7),
                (PlanetEnum.Venus, 4),
                (PlanetEnum.Mercury, 8),
                (PlanetEnum.Jupiter, 5),
                (PlanetEnum.Saturn, 6)
            }
        },
        {
            SignEnum.Saggitarius, new (PlanetEnum, uint)[]
            {
                (PlanetEnum.Jupiter, 12),
                (PlanetEnum.Venus, 5),
                (PlanetEnum.Mercury, 4),
                (PlanetEnum.Saturn, 5),
                (PlanetEnum.Mars, 4)
            }
        },
        {
            SignEnum.Capricorn, new (PlanetEnum, uint)[]
            {
                (PlanetEnum.Mercury, 7),
                (PlanetEnum.Jupiter, 7),
                (PlanetEnum.Venus, 8),
                (PlanetEnum.Saturn, 4),
                (PlanetEnum.Mars, 4)
            }
        },
        {
            SignEnum.Aquarius, new (PlanetEnum, uint)[]
            {
                (PlanetEnum.Mercury, 7),
                (PlanetEnum.Venus, 6),
                (PlanetEnum.Jupiter, 7),
                (PlanetEnum.Mars, 5),
                (PlanetEnum.Saturn, 5)
            }
        },
        {
            SignEnum.Pisces, new (PlanetEnum, uint)[]
            {
                (PlanetEnum.Venus, 12),
                (PlanetEnum.Jupiter, 4),
                (PlanetEnum.Mercury, 3),
                (PlanetEnum.Mars, 9),
                (PlanetEnum.Saturn, 2)
            }
        }
    };

    #endregion

    private readonly Dictionary<PlanetEnum, Planet> _planets;

    public TermBuilder(IRepository<Planet> planetRepository)
    {
        _planets = planetRepository.GetAll().ToDictionary(p => p.PlanetEnum, p => p);
    }

    public SubDivision BuildTerms(Sign sign)
    {
        return new SubDivision("Terms",
            CreateTermRanges(sign.Region.Start, Terms[sign.SignEnum].Select(t => (_planets[t.Planet], t.Range))));
    }

    private static IEnumerable<SubDivisionRange> CreateTermRanges(Degree start,
        IEnumerable<(Planet Planet, uint degrees)> planetsToDegrees)
    {
        var acc = start.Zodiacal.Degrees;
        foreach (var ptd in planetsToDegrees)
        {
            var degreeRange = new DegreeRange(acc, acc + ptd.degrees);
            acc += ptd.degrees;
            yield return new SubDivisionRange(degreeRange, ptd.Planet);
        }
    }
}