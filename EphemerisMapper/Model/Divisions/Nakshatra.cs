using EphemerisMapper.Extensions;
using EphemerisMapper.Model.Enums;
using EphemerisMapper.Model.ZodiacPosition;
using EphemerisMapper.Model.Mappers;

namespace EphemerisMapper.Model.Divisions;

public record Nakshatra : IDivision
{
    private static readonly Dictionary<DegreeRange, StarEnum> CuspToStar = GenerateCuspToStar();

    private static Dictionary<DegreeRange, StarEnum> GenerateCuspToStar()
    {
        var starRegion = new Degree(13, 20, 0);
        ;
        decimal acc = 0;
        return Enum.GetValues<StarEnum>()
            .ToDictionary(
                p => new DegreeRange(new Degree(acc).RoundToNearestWhole(),
                    new Degree(acc += starRegion.Dec).RoundToNearestWhole()), p => p);
    }

    public StarEnum StarEnum { get; }
    public DegreeRange Region { get; }
    public PlanetEnum StarLord { get; }
    public SubDivision<PlanetEnum> SubLords { get; }
    public string Name { get; }
    
    public IEnumerable<SubDivision> SubDivisions { get; }


    public Nakshatra(StarEnum starEnum)
    {
        StarEnum = starEnum;
        Region = GetRegion(starEnum);
        StarLord = starEnum.ToLord();
        SubLords = GenerateSubLords();
        Name = starEnum.ToString();
    }

    private static DegreeRange GetRegion(StarEnum starEnum) => CuspToStar.First(cts => cts.Value == starEnum).Key;

    private SubDivision<PlanetEnum> GenerateSubLords()
    {
        var planets = ReorderPlanets(Enum.GetValues<PlanetEnum>());

        var acc = Region.Start;

        return new SubDivision<PlanetEnum>("SubLord", planets
            .ToDictionary(p =>
            {
                var vm = new Degree(p.ToVimShottari() * StarEnumExtensions.StarRegion.Dec);
                var rng = new DegreeRange(acc.RoundToNearestWhole(), (acc + vm).RoundToNearestWhole());
                acc += vm;
                return rng;
            }, p => p));
    }

    private IEnumerable<PlanetEnum> ReorderPlanets(PlanetEnum[] planets)
    {
        var rightSide = planets.Where(p => (int)StarLord < (int)p);
        var leftSide = planets.Where(p => (int)StarLord > (int)p);

        return new[] { StarLord }.Concat(rightSide).Concat(leftSide).ToArray();
    }

    public override string ToString()
    {
        return $"Name: {StarEnum}, {nameof(Region)}: {Region}, {nameof(StarLord)}: {StarLord}";
    }
}