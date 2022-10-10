using EphemerisMapper.Model.Enums;
using EphemerisMapper.Model.ZodiacPosition;
using EphemerisMapper.Model.Mappers;

namespace EphemerisMapper.Model.Divisions;

public record Nakshatra
{
    public Star Star { get; }
    public DegreeRange StarRegion { get; }
    public Planet StarLord { get; }
    public SubDivision<Planet> SubLords { get; }

    public Nakshatra(Star star)
    {
        Star = star;
        StarRegion = star.ToDegreeRange();
        StarLord = star.ToLord();
        SubLords = GenerateSubLords();
    }

    private SubDivision<Planet> GenerateSubLords()
    {
        var planets = ReorderPlanets(Enum.GetValues<Planet>());

        var acc = StarRegion.Start;
        
        return new SubDivision<Planet>("SubLord", 1, planets
            .ToDictionary(p =>
            {
                var vm = new Degree(p.ToVimShottari() * StarMapper.StarRegion.Dec) ;
                var rng = new DegreeRange(acc.RoundToNearestWhole(), (acc + vm).RoundToNearestWhole());
                acc += vm;
                return rng;
            }, p => p));
    }

    private IEnumerable<Planet> ReorderPlanets(Planet[] planets)
    {
        var rightSide = planets.Where(p => (int)StarLord < (int)p);
        var leftSide = planets.Where(p => (int)StarLord > (int)p);

        return new[] { StarLord }.Concat(rightSide).Concat(leftSide).ToArray();
    }
    
}