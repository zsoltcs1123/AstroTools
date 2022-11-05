using AstroTools.Common.Extensions;
using AstroTools.Common.Factory;
using AstroTools.Common.Model.Degree;
using AstroTools.Common.Repository;
using AstroTools.Zodiac.Attributes;
using AstroTools.Zodiac.Model.CelestialObjects;
using AstroTools.Zodiac.Model.Divisions;
using AstroTools.Zodiac.Service.Cusp;
using AstroTools.Zodiac.Service.SubDivision;

namespace AstroTools.Zodiac.Factory;

public abstract class DivisionFactory<T, TU> : IFactory<T> where T : IDivision where TU : Enum
{
    private readonly Planet[] _planets;

    protected readonly Dictionary<TU, DegreeRange> DivisionToCusp;
    protected readonly ISubDivisionBuilder<T> SubDivisionBuilder;

    private Planet GetPlanet(PlanetEnum planetEnum) =>  _planets.First(p => p.PlanetEnum == planetEnum);
    private static PlanetEnum GetLordEnum(TU signEnum) => signEnum.Get<TraditionalLordAttribute>().Lord;
    protected Planet GetLord(TU signEnum) => GetPlanet(GetLordEnum(signEnum));

    protected DivisionFactory(
        Degree territory,
        ICuspGenerator<TU> cuspGenerator,
        IRepository<Planet> planetRepository,
        ISubDivisionBuilder<T> subDivisionBuilder)
    {
        DivisionToCusp = cuspGenerator.GenerateCusps(territory);
        _planets = planetRepository.GetAll().ToArray();
        SubDivisionBuilder = subDivisionBuilder;
    }

    public abstract IEnumerable<T> CreateAll();
}

