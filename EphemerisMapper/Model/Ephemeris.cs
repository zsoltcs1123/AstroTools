using EphemerisMapper.Model.Divisions;
using EphemerisMapper.Model.Enums;
using EphemerisMapper.Model.Units;

namespace EphemerisMapper.Model;

public class Ephemeris
{
    public Degree Longitude { get; }
    public PlanetEnum PlanetEnum { get; }
    public decimal Speed { get; }

    public bool IsRetrograde => Speed < 0;
    public DateTime Date { get; }

    public StarEnum StarEnum { get; }
    public PlanetEnum StarLord { get; }
    public Sign Sign { get; }
    public PlanetEnum SignLord { get; }
    public PlanetEnum SubLord { get; }

    public Ephemeris(PlanetEnum planetEnum, Degree longitude, decimal speed, DateTime date, Nakshatra nakshatra)
    {
        Longitude = longitude;
        PlanetEnum = planetEnum;
        Speed = speed;
        Date = date;
    }
}