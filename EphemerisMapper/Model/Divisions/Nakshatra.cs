using EphemerisMapper.Model.Enums;
using EphemerisMapper.Model.ZodiacPosition;
using EphemerisMapper.Model.Mappers;

namespace EphemerisMapper.Model.Divisions;

public record Nakshatra
{
    public Star Star { get; }
    public DegreeRange DegreeRange { get; }
    public Planet StarLord { get; }
    public SubDivision<Planet> SubLords { get; }

    public Nakshatra(Star star)
    {
        Star = star;
        DegreeRange = star.ToDegreeRange();
        StarLord = star.ToLord();
        SubLords = GenerateSubLords();
    }

    private SubDivision<Planet> GenerateSubLords()
    {
        var planets = ReorderPlanets(Enum.GetValues<Planet>());
        
        return new SubDivision<Planet>("SubLord", 1, planets
            .ToDictionary(p => p.ToDegreeRange(DegreeRange.Start.Dec), p => p));
    }

    private IEnumerable<Planet> ReorderPlanets(Planet[] planets)
    {
        var rightSide = planets.Where(p => (int)StarLord < (int)p);
        var leftSide = planets.Where(p => (int)StarLord > (int)p);

        return new[] { StarLord }.Concat(rightSide).Concat(leftSide).ToArray();
    }
    
}