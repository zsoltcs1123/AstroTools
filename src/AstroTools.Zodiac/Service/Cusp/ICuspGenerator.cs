using AstroTools.Common.Model.Degree;

namespace AstroTools.Zodiac.Service.Cusp;

public interface ICuspGenerator<T> where T : Enum
{
    Dictionary<T, DegreeRange> GenerateCusps(Degree territory);
}