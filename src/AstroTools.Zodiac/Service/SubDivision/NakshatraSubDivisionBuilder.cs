using AstroTools.CelestialObjects.Model;
using AstroTools.Common.Model.Degree;
using AstroTools.Common.Repository;
using AstroTools.Zodiac.Model.Divisions;
using AstroTools.Zodiac.Model.SubDivisions;

namespace AstroTools.Zodiac.Service.SubDivision;

public class NakshatraSubDivisionBuilder : ISubDivisionBuilder<Nakshatra>
{
    private static readonly Degree StarRegion = new(13, 20, 0);
    private readonly SortedList<int, Planet> _planets;

    public NakshatraSubDivisionBuilder(IRepository<Planet> planetRepository)
    {
        _planets = InitialOrder(planetRepository.GetAll().ToArray());
    }

    private static SortedList<int, Planet> InitialOrder(Planet[] planets)
    {
        return new SortedList<int, Planet>
        {
            { 1, planets.First(p => p.PlanetEnum == PlanetEnum.Ketu) },
            { 2, planets.First(p => p.PlanetEnum == PlanetEnum.Venus) },
            { 3, planets.First(p => p.PlanetEnum == PlanetEnum.Sun) },
            { 4, planets.First(p => p.PlanetEnum == PlanetEnum.Moon) },
            { 5, planets.First(p => p.PlanetEnum == PlanetEnum.Mars) },
            { 6, planets.First(p => p.PlanetEnum == PlanetEnum.Rahu) },
            { 7, planets.First(p => p.PlanetEnum == PlanetEnum.Jupiter) },
            { 8, planets.First(p => p.PlanetEnum == PlanetEnum.Saturn) },
            { 9, planets.First(p => p.PlanetEnum == PlanetEnum.Mercury) },
        };
    }

    private IEnumerable<Planet> ReorderPlanets(Planet starLord)
    {
        int starLordIndex = _planets.First(p => p.Value.PlanetEnum == starLord.PlanetEnum).Key;

        var rightSide = _planets
            .Where(p => p.Key < starLordIndex)
            .Select(p => p.Value);

        var leftSide = _planets
            .Where(p => p.Key > starLordIndex)
            .Select(p => p.Value);

        return new[] { starLord }.Concat(leftSide).Concat(rightSide).ToArray();
    }

    private Model.SubDivisions.SubDivision CreateSubDivisions(Planet lord, Degree startDeg, string name, Degree region)
    {
        var planets = ReorderPlanets(lord);

        var acc = startDeg;

        return new Model.SubDivisions.SubDivision("SubLord", planets
            .Select(p =>
            {
                var vm = new Degree(p.VimShottariRatio * region.Dec).RoundToNearestWhole();
                var start = acc.RoundToNearestWhole();
                var end = (acc += vm).RoundToNearestWhole();
                var rng = new DegreeRange(start, end);
                return new SubDivisionRange(rng, p, CreateSubDivisions(p, start, "SubSubLord",vm));
            }));
    }

    public IEnumerable<Model.SubDivisions.SubDivision> BuildSubDivisions(Nakshatra nakshatra)
    {
        return new[] { CreateSubDivisions(nakshatra.Lord, nakshatra.Region.Start, "SubLord", StarRegion) };
    }
}