using System.Linq.Expressions;
using AstroTools.Common.Factory;
using AstroTools.Common.Repository;
using AstroTools.Zodiac.Model.CelestialObjects;

namespace AstroTools.Zodiac.Service.Repository;

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

    public IEnumerable<Planet> Get(Expression<Func<Planet, bool>>? filter = null, Func<IQueryable<Planet>, IOrderedQueryable<Planet>>? orderBy = null, string includeProperties = "")
    {
        throw new NotImplementedException();
    }
}