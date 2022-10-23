using EphemerisMapper.Model.CelestialObjects;
using EphemerisMapper.Model.Enums;

namespace EphemerisMapper.Model.Units;

public class Ephemeris
{
    public Degree Longitude { get; }
    public PlanetEnum PlanetEnum { get; }
    public decimal Speed { get; }
    public bool IsRetrograde => Speed < 0;
    public DateTime Date { get; }

    private Dictionary<string, IMappable> Mapped { get; }

    public Ephemeris(PlanetEnum planetEnum, Degree longitude, decimal speed, DateTime date, Dictionary<string, IMappable> mapped)
    {
        Longitude = longitude;
        PlanetEnum = planetEnum;
        Speed = speed;
        Date = date;
        Mapped = mapped;
    }
}