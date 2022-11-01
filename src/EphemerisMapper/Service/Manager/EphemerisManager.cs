using EphemerisMapper.Model.Enums;

namespace EphemerisMapper.Service.Manager;

public class EphemerisManager
{
    private readonly List<Model.Units.Ephemeris> _ephemerides;
    private readonly DateTime _firstDate;
    private readonly DateTime _lastDate;

    public EphemerisManager(IEnumerable<Model.Units.Ephemeris> ephemerides)
    {
        _ephemerides = ephemerides.ToList();
        _firstDate = _ephemerides.Min(e => e.Date).Date;
        _lastDate = _ephemerides.Max(e => e.Date).Date;
    }

    public Dictionary<DateTime, Model.Units.Ephemeris> GetByPlanet(PlanetEnum planetEnum, DateTime? startDate = null,
        DateTime? endDate = null)
    {
        return _ephemerides
            .Where(e => e.PlanetEnum == planetEnum)
            .Where(e => e.Date >= (startDate ?? _firstDate) && e.Date <= (endDate ?? _lastDate))
            .ToDictionary(e => e.Date, e => e);
    }
    
    public IEnumerable<IGrouping<DateTime, Model.Units.Ephemeris>> GetByDate(DateTime? startDate = null,
        DateTime? endDate = null)
    {
        return _ephemerides
            .Where(e => e.Date >= (startDate ?? _firstDate) && e.Date <= (endDate ?? _lastDate))
            .GroupBy(e => e.Date, e => e);
    }
}