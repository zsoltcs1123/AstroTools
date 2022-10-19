using EphemerisMapper.Model.ZodiacPosition;

namespace EphemerisMapper.Service.Divisions.Cusps;

public class CuspGenerator<T> : ICuspGenerator<T> where T : struct, Enum
{
    public Dictionary<T, DegreeRange> GenerateCusps(Degree territory)
    {
        decimal acc = 0;
        return Enum.GetValues<T>()
            .ToDictionary(
                p => p,
                p => new DegreeRange(new Degree(acc).RoundToNearestWhole(),
                    new Degree(acc += territory.Dec).RoundToNearestWhole()));
    }
}