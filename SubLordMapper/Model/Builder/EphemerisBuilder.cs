using SubLordMapper.Model.DataTransfer;
using SubLordMapper.Model.Enums;
using SubLordMapper.Model.Mappers;
using SubLordMapper.Model.ZodiacPosition;

namespace SubLordMapper.Model.Builder;

public class EphemerisBuilder
{
    public IEnumerable<EphemerisNew> Build(MultiEphemerisDto dto)
    {
        return Enum.GetValues<Planet>()
            .Where(p => p != Planet.Moon)
            .Select(p =>
            {
                var degrees = GetDegreesForPlanet(p, dto);
                return new EphemerisNew(p, degrees, 0, dto.Date, degrees.ToSubLord());
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