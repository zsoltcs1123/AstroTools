using AstroTools.Common.Model;
using AstroTools.Common.Model.Degree;
using AstroTools.Zodiac.Model.CelestialObjects;

namespace AstroTools.Zodiac.Service;

public interface IZodiac
{
    IEnumerable<MappedData> Map(Planet planet, Degree degree);
}