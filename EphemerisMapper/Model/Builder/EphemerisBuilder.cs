using EphemerisMapper.Model.DataTransfer;
using EphemerisMapper.Model.Divisions;
using EphemerisMapper.Model.Enums;
using EphemerisMapper.Model.ZodiacPosition;

namespace EphemerisMapper.Model.Builder;

public class EphemerisBuilder
{
    private static readonly List<Nakshatra> Nakshatras = Enum.GetValues<Star>()
        .Select(s => new Nakshatra(s)).ToList();

    public IEnumerable<Ephemeris> Build(MultiEphemerisDto dto)
    {
        return Enum.GetValues<Planet>()
            .Where(p => p != Planet.Moon)
            .Select(p =>
            {
                var degrees = GetDegreesForPlanet(p, dto);
                return new Ephemeris(p, degrees, 0, dto.Date, Nakshatras.First(n => n.StarRegion.Contains(degrees)));
            });
    }

    private static Degree GetDegreesForPlanet(Planet planet, MultiEphemerisDto dto) => new(planet switch
    {
        Planet.Ketu => dto.SouthNode,
        Planet.Venus => dto.Venus,
        Planet.Sun => dto.Sun,
        Planet.Mars => dto.Mars,
        Planet.Rahu => dto.MeanNode,
        Planet.Jupiter => dto.MeanNode,
        Planet.Saturn => dto.MeanNode,
        Planet.Mercury => dto.MeanNode,
        _ => 0m
    });
}