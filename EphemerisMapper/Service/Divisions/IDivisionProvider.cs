using EphemerisMapper.Model.Divisions;
using EphemerisMapper.Model.ZodiacPosition;

namespace EphemerisMapper.Service.Divisions;

public interface IDivisionProvider<out T> where T : IDivision
{
    T Get(Degree degree);
}