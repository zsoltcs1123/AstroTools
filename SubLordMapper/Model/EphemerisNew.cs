using System.Reflection.Metadata.Ecma335;
using SubLordMapper.Model.Divisions;
using SubLordMapper.Model.Enums;
using SubLordMapper.Model.Mappers;
using SubLordMapper.Model.ZodiacPosition;

namespace SubLordMapper.Model;

public class EphemerisNew
{
    private readonly SubLordNew _subLord;
    public Degree Longitude { get; }
    public Planet Planet { get; }
    public decimal Speed { get; }

    public bool IsRetrograde => Speed < 0;
    public DateTime Date { get; }

    public Star Star => _subLord.Star;
    public Planet StarLord => _subLord.StarLord;
    public Sign Sign => _subLord.Sign;
    public Planet SignLord => _subLord.SignLord;

    public Planet SubLord => _subLord.Sub;

    public EphemerisNew(Planet planet, Degree longitude, decimal speed, DateTime date, SubLordNew subLord)
    {
        _subLord = subLord;
        Longitude = longitude;
        Planet = planet;
        Speed = speed;
        Date = date;
    }
}