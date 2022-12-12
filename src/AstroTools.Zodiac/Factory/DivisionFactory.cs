using AstroTools.Common.Extensions;
using AstroTools.Common.Factory;
using AstroTools.Common.Model.Degree;
using AstroTools.Common.Repository;
using AstroTools.Zodiac.Attributes;
using AstroTools.Zodiac.Builder;
using AstroTools.Zodiac.Builder.Cusp;
using AstroTools.Zodiac.Model.CelestialObjects;
using AstroTools.Zodiac.Model.Divisions;

namespace AstroTools.Zodiac.Factory;

public class DivisionFactory<T, TEnum> : IFactory<T> where T : class, IDivision where TEnum : struct, Enum
{
    private readonly Planet[] _planets;

    private readonly Dictionary<TEnum, DegreeRange> _divisionToCusp;
    private readonly Func<TEnum, DegreeRange, Planet, T> _divisionCreatorFunc;
    private readonly ISubDivisionBuilder<T> _subDivisionBuilder;

    private Planet GetPlanet(PlanetEnum planetEnum) =>  _planets.First(p => p.PlanetEnum == planetEnum);
    private static PlanetEnum GetLordEnum(TEnum @enum) => @enum.Get<TraditionalLordAttribute>().Lord;
    private Planet GetLord(TEnum @enum) => GetPlanet(GetLordEnum(@enum));

    public DivisionFactory(
        Degree territory,
        Func<TEnum, DegreeRange, Planet, T> divisionCreatorFunc,
        ICuspGenerator<TEnum> cuspGenerator,
        IRepository<Planet> planetRepository,
        ISubDivisionBuilder<T> subDivisionBuilder)
    {
        _divisionToCusp = cuspGenerator.GenerateCusps(territory);
        _planets = planetRepository.GetAll().ToArray();
        _divisionCreatorFunc = divisionCreatorFunc;
        _subDivisionBuilder = subDivisionBuilder;
    }

    public IEnumerable<T> CreateAll()
    {
        var divisions = Enum.GetValues<TEnum>()
            .Select(s => _divisionCreatorFunc(s, _divisionToCusp[s], GetLord(s))).ToArray();
        
        foreach (var division in divisions)
        {
            division.SubDivisions = _subDivisionBuilder.BuildSubDivisions(division);
        }

        return divisions;

    }
}

