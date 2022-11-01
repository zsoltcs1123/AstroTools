using AstroTools.CelestialObjects.Model;
using AstroTools.Common.Factory;
using AstroTools.Common.Model.Degree;
using AstroTools.Ephemeris.Model.DataTransfer;
using AstroTools.Zodiac.Service;

namespace AstroTools.Ephemeris.Factory;

public class EphemerisFactory : IParameterizedFactory<Model.Ephemeris, EphemerisDto>
{
    private readonly IZodiac _zodiac;

    public EphemerisFactory(IZodiac zodiac)
    {
        _zodiac = zodiac;
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
                return new Model.Ephemeris(p, degrees, 0, dto.Date, _zodiac.Map(degrees));
            });
    }

    public IEnumerable<Model.Ephemeris> Build(MoonEphemerisDto dto)
    {
        return Enum.GetValues<PlanetEnum>()
            .Where(p => p == PlanetEnum.Moon)
            .Select(p =>
            {
                var degrees = GetDegreesForPlanet(p, dto);
                return new Model.Ephemeris(p, degrees, 0, dto.Date, _zodiac.Map(degrees));
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