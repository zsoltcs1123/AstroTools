using EphemerisMapper.Model.CelestialObjects;
using EphemerisMapper.Model.Divisions;
using EphemerisMapper.Model.Enums;
using EphemerisMapper.Model.SubDivisions;
using EphemerisMapper.Model.Units;
using EphemerisMapper.Service.Repository;
using EphemerisMapper.Service.Repository.CelestialObjects;

namespace EphemerisMapper.Service.Builder.SubDivision;

public class NakshatraSubDivisionBuilder : ISubDivisionBuilder<Nakshatra>
{
    private static readonly Degree StarRegion = new(13, 20, 0);
    private readonly SortedList<int, Planet> _planets;

    public NakshatraSubDivisionBuilder(ICelestialObjectRepository<Planet> planetRepository)
    {
        _planets = InitialOrder(planetRepository.GetAll().ToArray());
    }

    private static SortedList<int, Planet> InitialOrder(Planet[] planets)
    {
        return new SortedList<int, Planet>
        {
            { 1,  planets.First(p => p.PlanetEnum == PlanetEnum.Ketu)},
            { 2, planets.First(p => p.PlanetEnum == PlanetEnum.Venus)},
            { 3, planets.First(p => p.PlanetEnum == PlanetEnum.Sun)},
            { 4, planets.First(p => p.PlanetEnum == PlanetEnum.Moon)},
            { 5, planets.First(p => p.PlanetEnum == PlanetEnum.Mars)},
            { 6, planets.First(p => p.PlanetEnum == PlanetEnum.Rahu)},
            { 7, planets.First(p => p.PlanetEnum == PlanetEnum.Jupiter)},
            { 8, planets.First(p => p.PlanetEnum == PlanetEnum.Saturn)},
            { 9, planets.First(p => p.PlanetEnum == PlanetEnum.Mercury)},
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

        return new[] { starLord }.Concat(rightSide).Concat(leftSide).ToArray();
    }
    
    private Model.SubDivisions.SubDivision CreateSubLords(Nakshatra nakshatra)
    {
        var planets = ReorderPlanets(nakshatra.Lord);

        var acc = nakshatra.Region.Start;

        return new Model.SubDivisions.SubDivision("SubLord", planets
            .Select(p =>
            {
                var vm = new Degree(p.VimShottariRatio * StarRegion.Dec);
                var rng = new DegreeRange(acc.RoundToNearestWhole(), (acc + vm).RoundToNearestWhole());
                acc += vm;
                return new SubDivisionRange(rng, p);
            }));
    }

    public IEnumerable<Model.SubDivisions.SubDivision> BuildSubDivisions(Nakshatra nakshatra)
    {
        return new[] { CreateSubLords(nakshatra) };
    }

}