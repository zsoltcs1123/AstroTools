using EphemerisMapper.Model.CelestialObjects;

namespace EphemerisMapper.Service.Builder;

public interface ICelestialObjectBuilder<out T> where T: ICelestialObject
{
    IEnumerable<T> BuildCelestialObjects();
}