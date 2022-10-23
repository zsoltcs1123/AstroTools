using EphemerisMapper.Model;
using EphemerisMapper.Model.CelestialObjects;
using EphemerisMapper.Model.Units;

namespace EphemerisMapper.Service.Zodiac;

public interface IZodiac
{
    Dictionary<string, IMappable> Map(Degree degree);
}