using EphemerisMapper.Model.CelestialObjects;

namespace EphemerisMapper.Service.Builder.CelestialObjects;

public interface ICelestialObjectBuilder<out T> where T: ICelestialObject
{
    IEnumerable<T> BuildCelestialObjects();
}