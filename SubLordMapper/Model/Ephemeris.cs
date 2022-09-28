using SubLordMapper.Model.DataTransfer;

namespace SubLordMapper.Model;

public class Ephemeris
{
    public DateTime Date { get; }
    public Dictionary<Planet, double> Longitudes { get; } = new();

    public Ephemeris(MoonEphemerisDto moonEphemerisDto)
    {
        Date = moonEphemerisDto.Date;
        Longitudes.Add(Planet.Moon, moonEphemerisDto.Moon);
    }

    public Ephemeris(MultiEphemerisDto multiEphemerisDto)
    {
        Date = multiEphemerisDto.Date;
        Longitudes.Add(Planet.Sun, multiEphemerisDto.Sun);
        Longitudes.Add(Planet.Mercury, multiEphemerisDto.Mercury);
        Longitudes.Add(Planet.Venus, multiEphemerisDto.Venus);
        Longitudes.Add(Planet.Mars, multiEphemerisDto.Mars);
        Longitudes.Add(Planet.Jupiter, multiEphemerisDto.Jupiter);
        Longitudes.Add(Planet.Saturn, multiEphemerisDto.Saturn);
        Longitudes.Add(Planet.Rahu, multiEphemerisDto.MeanNode);
        Longitudes.Add(Planet.Ketu, multiEphemerisDto.SouthNode);
    }

    public Dictionary<Planet, (double, SubLord?)> GenerateSubLords(List<SubLord> subLordTable)
    {
        return Longitudes.ToDictionary(
            kvp => kvp.Key,
            kvp =>
                (kvp.Value,
                    subLordTable.FirstOrDefault(s => kvp.Value >= s.Degrees.Start && kvp.Value <= s.Degrees.End)));
    }
}