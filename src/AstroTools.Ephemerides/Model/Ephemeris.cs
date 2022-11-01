using AstroTools.CelestialObjects.Model;
using AstroTools.Common.Model;
using AstroTools.Common.Model.Degree;

namespace AstroTools.Ephemerides.Model;

public class Ephemeris
{
    public Degree Longitude { get; }
    public PlanetEnum PlanetEnum { get; }
    public decimal Speed { get; }
    public bool IsRetrograde => Speed < 0;
    public DateTime Date { get; }

    public Dictionary<string, IMappable> MappedData { get; }

    public Ephemeris(PlanetEnum planetEnum, Degree longitude, decimal speed, DateTime date, Dictionary<string, IMappable> mappedData)
    {
        Longitude = longitude;
        PlanetEnum = planetEnum;
        Speed = speed;
        Date = date;
        MappedData = mappedData;
    }
}