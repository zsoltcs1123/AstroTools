using SubLordMapper.Model.Enums;
using SubLordMapper.Model.Mappers;
using SubLordMapper.Model.ZodiacPosition;

namespace SubLordMapper.Model.Divisions;

public record Nakshatra
{
    public Star Star { get; }
    public DegreeRange DegreeRange { get; }
    public Planet StarLord { get; }

    public List<SubDivision> SubDivisions { get; }

    public Nakshatra(Star star)
    {
        Star = star;
        DegreeRange = star.ToDegreeRange();
        StarLord = star.ToLord();
        var subLords = GenerateSubLordDivision();
        SubDivisions = new List<SubDivision> { subLords };
    }

    private SubDivision GenerateSubLordDivision()
    {
        var planets = ReorderPlanets(Enum.GetValues<Planet>());
        
        return new SubDivision("SubLord", 1, planets.ToDictionary(p => p.ToDegreeRange(DegreeRange.Start.Decimal), p => p));
    }

    private IEnumerable<Planet> ReorderPlanets(Planet[] planets)
    {
        var rightSide = planets.Where(p => (int)StarLord < (int)p);
        var leftSide = planets.Where(p => (int)StarLord > (int)p);

        return new[] { StarLord }.Concat(rightSide).Concat(leftSide).ToArray();
    }
}