using AstroTools.Common.Model;
using AstroTools.Common.Model.Degree;
using AstroTools.Zodiac.Model.CelestialObjects;

namespace AstroTools.Ephemerides.Model;

public class Ephemeris
{
    public Degree Longitude { get; }
    public Planet Planet { get; }
    public decimal Speed { get; }
    public bool IsRetrograde => Speed < 0;
    public DateTime Date { get; }

    public Dictionary<string, MappedData> MappedData { get; }

    public Ephemeris(Planet planet, Degree longitude, decimal speed, DateTime date, IEnumerable<MappedData> mappedData)
    {
        Longitude = longitude;
        Planet = planet;
        Speed = speed;
        Date = date;
        MappedData = mappedData.ToDictionary(m => m.Name, m => m);
    }
}