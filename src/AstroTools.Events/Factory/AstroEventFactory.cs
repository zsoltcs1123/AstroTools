using AstroTools.Common.Factory;
using AstroTools.Common.Model;
using AstroTools.Ephemerides.Model;
using AstroTools.Events.Model;
using AstroTools.Zodiac.Model.CelestialObjects;

namespace AstroTools.Events.Factory;

public class AstroEventFactory : IParameterizedFactory<AstroEvent, IEnumerable<Ephemeris>>
{
    public IEnumerable<AstroEvent> CreateAll(IEnumerable<Ephemeris> ephemerides)
    {
        var ret = new List<AstroEvent>();

        var ephems = ephemerides.OrderBy(e => e.Planet.PlanetEnum).ThenBy(e => e.Date).ToArray();
        for (var i = 1; i < ephems.Length; i++)
        {
            if (ephems[i].Planet == ephems[i - 1].Planet)
            {
                var changed = FilterForChangeInMappedData(ephems[i], ephems[i - 1]);
                ret.AddRange(CreateAstroEvents(ephems[i].Planet, ephems[i], changed));
            }
        }

        return ret;
    }

    private static IEnumerable<(string Key, MappedData Current, MappedData Previous)> FilterForChangeInMappedData(
        Ephemeris current, Ephemeris previous)
    {
        return current.MappedData
            .Where(mapped => previous.MappedData[mapped.Key].Mappable != mapped.Value.Mappable)
            .Select(mapped => (mapped.Key, mapped.Value, previous.MappedData[mapped.Key]));
    }

    private static IEnumerable<AstroEvent> CreateAstroEvents(Planet planet, Ephemeris current,
        IEnumerable<(string Key, MappedData Current, MappedData Previous)> changed)
    {
        return changed
            .Select(changes =>
                new AstroEvent(planet, current.Date, $"{current.Planet.PlanetEnum} {changes.Key}Change",
                    CreateDescription(changes.Current, changes.Previous)));
    }

    private static string CreateDescription(MappedData current, MappedData previous)
    {
        return $"{previous.Mappable.Name} [{previous.Status}] -> {current.Mappable.Name} [{current.Status}]"
            .Replace(" []", "");
    }
}