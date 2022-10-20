using EphemerisMapper.Model.CelestialObjects;

namespace EphemerisMapper.Service.Repository.CelestialObjects;

public interface ICelestialObjectRepository<out T> where T: ICelestialObject
{
    IEnumerable<T> GetAll();
}