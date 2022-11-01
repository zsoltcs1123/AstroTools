using EphemerisMapper.Extensions;
using EphemerisMapper.Model.Attributes;
using EphemerisMapper.Model.CelestialObjects;
using EphemerisMapper.Model.Divisions;
using EphemerisMapper.Model.Enums;
using EphemerisMapper.Model.Units;
using EphemerisMapper.Service.Builder.Cusp;
using EphemerisMapper.Service.Repository;
using EphemerisMapper.Service.Repository.CelestialObjects;

namespace EphemerisMapper.Service.Builder.Division;

public class SignBuilder : IDivisionBuilder<Sign>
{
    private static readonly Degree Territory = new(30);
    
    private readonly Dictionary<SignEnum, DegreeRange> _signToCusp;
    private readonly Planet[] _planets;

    public SignBuilder(
        ICuspGenerator<SignEnum> cuspGenerator,
        ICelestialObjectRepository<Planet> planetRepository)
    {
        _signToCusp = cuspGenerator.GenerateCusps(Territory);
        _planets = planetRepository.GetAll().ToArray();
    }

    private Planet GetPlanet(PlanetEnum planetEnum) => _planets.First(p => p.PlanetEnum == planetEnum);
    private static PlanetEnum GetLordEnum(SignEnum signEnum) => signEnum.Get<TraditionalLordAttribute>().Lord;

    public IEnumerable<Sign> BuildDivisions()
    {
        return Enum.GetValues<SignEnum>()
            .Select(s => new Sign(s.ToString(), _signToCusp[s], GetPlanet(GetLordEnum(s)))).ToArray();
    }
}