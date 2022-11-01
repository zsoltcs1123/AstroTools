using AstroTools.CelestialObjects.Model;
using AstroTools.Common.Factory;
using AstroTools.Common.Repository;

namespace AstroTools.CelestialObjects.Repository;

public class PlanetRepository : IRepository<Planet>
{
    private readonly IEnumerable<Planet> _planets;

    public PlanetRepository(IFactory<Planet> planetFactory)
    {
        _planets = planetFactory.CreateAll();
    }

    public IEnumerable<Planet> GetAll()
    {
        return _planets;
    }
}