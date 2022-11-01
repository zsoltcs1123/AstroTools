using EphemerisMapper.Extensions;
using EphemerisMapper.Model.Attributes;
using EphemerisMapper.Model.CelestialObjects;
using EphemerisMapper.Model.Divisions;
using EphemerisMapper.Model.Enums;
using EphemerisMapper.Model.Units;
using EphemerisMapper.Service.Builder.Cusp;
using EphemerisMapper.Service.Builder.SubDivision;
using EphemerisMapper.Service.Repository;
using EphemerisMapper.Service.Repository.CelestialObjects;

namespace EphemerisMapper.Service.Builder.Division;

public abstract class DivisionBuilder<T, TU> : IDivisionBuilder<T> where T : IDivision where TU : Enum
{
    private readonly Planet[] _planets;

    protected readonly Dictionary<TU, DegreeRange> DivisionToCusp;
    protected readonly ISubDivisionBuilder<T> SubDivisionBuilder;

    private Planet GetPlanet(PlanetEnum planetEnum) =>  _planets.First(p => p.PlanetEnum == planetEnum);
    private static PlanetEnum GetLordEnum(TU signEnum) => signEnum.Get<TraditionalLordAttribute>().Lord;
    protected Planet GetLord(TU signEnum) => GetPlanet(GetLordEnum(signEnum));

    protected DivisionBuilder(
        Degree territory,
        ICuspGenerator<TU> cuspGenerator,
        ICelestialObjectRepository<Planet> planetRepository,
        ISubDivisionBuilder<T> subDivisionBuilder)
    {
        DivisionToCusp = cuspGenerator.GenerateCusps(territory);
        _planets = planetRepository.GetAll().ToArray();
        SubDivisionBuilder = subDivisionBuilder;
    }

    public abstract IEnumerable<T> BuildDivisions();
}

