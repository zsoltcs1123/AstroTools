using AstroTools.Common.Factory;
using AstroTools.Common.Model.Degree;
using AstroTools.Ephemerides.Model.DataTransfer;
using AstroTools.Zodiac.Model.CelestialObjects;
using AstroTools.Zodiac.Service;

namespace AstroTools.Ephemerides.Factory;

public class EphemerisFactory : IParameterizedFactory<Model.Ephemeris, EphemerisDto>
{
    private readonly IZodiac _zodiac;
    private readonly Dictionary<PlanetEnum, Planet> _planets;

    public EphemerisFactory(IZodiac zodiac, IFactory<Planet> planetFactory)
    {
        _zodiac = zodiac;
        _planets = planetFactory.CreateAll().ToDictionary(p => p.PlanetEnum, p => p);
    }

    public IEnumerable<Model.Ephemeris> CreateAll(EphemerisDto parameter)
    {
        return parameter switch
        {
            MultiEphemerisDto multi => Build(multi),
            MoonEphemerisDto moon => Build(moon),
            _ => Array.Empty<Model.Ephemeris>()
        };
    }

    public IEnumerable<Model.Ephemeris> Build(MultiEphemerisDto dto)
    {
        return Enum.GetValues<PlanetEnum>()
            .Where(p => p != PlanetEnum.Moon)
            .Select(p =>
            {
                var degrees = GetDegreesForPlanet(p, dto);
                var planet = _planets[p];
                return new Model.Ephemeris(planet, degrees, 0, dto.Date, _zodiac.Map(planet, degrees));
            });
    }

    public IEnumerable<Model.Ephemeris> Build(MoonEphemerisDto dto)
    {
        return Enum.GetValues<PlanetEnum>()
            .Where(p => p == PlanetEnum.Moon)
            .Select(p =>
            {
                var degrees = GetDegreesForPlanet(p, dto);
                var planet = _planets[p];
                return new Model.Ephemeris(planet, degrees, 0, dto.Date, _zodiac.Map(planet, degrees));
            });
    }

    private static Degree GetDegreesForPlanet(PlanetEnum planetEnum, MultiEphemerisDto dto) => new(planetEnum switch
    {
        PlanetEnum.Ketu => dto.SouthNode,
        PlanetEnum.Venus => dto.Venus,
        PlanetEnum.Sun => dto.Sun,
        PlanetEnum.Mars => dto.Mars,
        PlanetEnum.Rahu => dto.MeanNode,
        PlanetEnum.Jupiter => dto.Jupiter,
        PlanetEnum.Saturn => dto.Saturn,
        PlanetEnum.Mercury => dto.Mercury,
        _ => 0m
    });

    private static Degree GetDegreesForPlanet(PlanetEnum planetEnum, MoonEphemerisDto dto) => new(planetEnum switch
    {
        PlanetEnum.Moon => dto.Moon,
        _ => 0m
    });
}