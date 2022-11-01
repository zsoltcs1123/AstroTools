using AstroTools.Common.Factory;
using AstroTools.Common.Model;
using AstroTools.Ephemerides.Model;
using AstroTools.Events.Model;

namespace AstroTools.Events.Factory;

public class AstroEventFactory : IParameterizedFactory<AstroEvent, IEnumerable<Ephemeris>>
{
    public IEnumerable<AstroEvent> CreateAll(IEnumerable<Ephemeris> ephemerides)
    {
        var ret = new List<AstroEvent>();

        var ephems = ephemerides.OrderBy(e => e.PlanetEnum).ThenBy(e => e.Date).ToArray();
        for (var i = 1; i < ephems.Length; i++)
        {
            if (ephems[i].PlanetEnum == ephems[i - 1].PlanetEnum)
            {
                var changed = FilterForChangeInMappedData(ephems[i], ephems[i - 1]);
                ret.AddRange(CreateAstroEvents(ephems[i], changed));
            }
        }

        return ret;
    }

    private static IEnumerable<(string Key, IMappable Current, IMappable Previous)> FilterForChangeInMappedData(
        Ephemeris current, Ephemeris previous)
    {
        return current.MappedData
            .Where(mapped => previous.MappedData[mapped.Key] != mapped.Value)
            .Select(mapped => (mapped.Key, mapped.Value, previous.MappedData[mapped.Key]));
    }

    private static IEnumerable<AstroEvent> CreateAstroEvents(Ephemeris current,
        IEnumerable<(string Key, IMappable Current, IMappable Previous)> changed)
    {
        return changed
            .Select(changes =>
                new AstroEvent(current.Date, $"{current.PlanetEnum} {changes.Key}Change",
                    CreateDescription(changes.Current, changes.Previous)));
    }

    private static string CreateDescription(IMappable current, IMappable previous)
    {
        return $"{previous.Name} -> {current.Name}";
    }
}