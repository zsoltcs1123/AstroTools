using AstroTools.Common.Model;
using AstroTools.Common.Model.Degree;

namespace AstroTools.Zodiac.Service;

public interface IZodiac
{
    Dictionary<string, IMappable> Map(Degree degree);
}