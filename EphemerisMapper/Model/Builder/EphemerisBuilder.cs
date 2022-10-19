using EphemerisMapper.Model.DataTransfer;
using EphemerisMapper.Model.Divisions;
using EphemerisMapper.Model.Enums;
using EphemerisMapper.Model.ZodiacPosition;

namespace EphemerisMapper.Model.Builder;

public class EphemerisBuilder
{
    private static readonly List<Nakshatra> Nakshatras = Enum.GetValues<StarEnum>()
        .Select(s => new Nakshatra(s)).ToList();

    public IEnumerable<Ephemeris> Build(MultiEphemerisDto dto)
    {
        return Enum.GetValues<PlanetEnum>()
            .Where(p => p != PlanetEnum.Moon)
            .Select(p =>
            {
                var degrees = GetDegreesForPlanet(p, dto);
                return new Ephemeris(p, degrees, 0, dto.Date, Nakshatras.First(n => n.Region.Contains(degrees)));
            });
    }

    private static Degree GetDegreesForPlanet(PlanetEnum planetEnum, MultiEphemerisDto dto) => new(planetEnum switch
    {
        PlanetEnum.Ketu => dto.SouthNode,
        PlanetEnum.Venus => dto.Venus,
        PlanetEnum.Sun => dto.Sun,
        PlanetEnum.Mars => dto.Mars,
        PlanetEnum.Rahu => dto.MeanNode,
        PlanetEnum.Jupiter => dto.MeanNode,
        PlanetEnum.Saturn => dto.MeanNode,
        PlanetEnum.Mercury => dto.MeanNode,
        _ => 0m
    });
}