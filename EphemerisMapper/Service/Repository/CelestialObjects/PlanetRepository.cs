using EphemerisMapper.Model.CelestialObjects;
using EphemerisMapper.Service.Builder.CelestialObjects;

namespace EphemerisMapper.Service.Repository.CelestialObjects;

public class PlanetRepository : ICelestialObjectRepository<Planet>
{
    private readonly IEnumerable<Planet> _planets;

    public PlanetRepository(ICelestialObjectBuilder<Planet> planetBuilder)
    {
        _planets = planetBuilder.BuildCelestialObjects();
    }

    public IEnumerable<Planet> GetAll()
    {
        return _planets;
    }
}