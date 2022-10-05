using EphemerisMapper.Model.Divisions;
using EphemerisMapper.Model.Enums;
using EphemerisMapper.Model.ZodiacPosition;
using EphemerisMapper.Model.Mappers;

namespace EphemerisMapper.Model;

public class Ephemeris
{
    public Degree Longitude { get; }
    public Planet Planet { get; }
    public decimal Speed { get; }

    public bool IsRetrograde => Speed < 0;
    public DateTime Date { get; }

    public Star Star { get; }
    public Planet StarLord { get; }
    public Sign Sign { get; }
    public Planet SignLord { get; }
    public Planet SubLord { get; }

    public Ephemeris(Planet planet, Degree longitude, decimal speed, DateTime date, Nakshatra nakshatra)
    {
        Longitude = longitude;
        Planet = planet;
        Speed = speed;
        Date = date;
        Star = longitude.ToStar();
        StarLord = Star.ToLord();
        Sign = longitude.ToSign();
        SignLord = Sign.ToLord();
        SubLord = nakshatra.SubLords.Ranges.First(rng => rng.Key.Contains(longitude)).Value;
    }
}