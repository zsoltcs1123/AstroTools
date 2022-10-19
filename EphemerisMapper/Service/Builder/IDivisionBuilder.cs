using EphemerisMapper.Model.Divisions;

namespace EphemerisMapper.Service.Builder;

public interface IDivisionBuilder<out T> where T: IDivision
{
    IEnumerable<T> BuildDivisions();
}