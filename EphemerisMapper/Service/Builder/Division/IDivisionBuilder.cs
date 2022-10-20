using EphemerisMapper.Model.Divisions;

namespace EphemerisMapper.Service.Builder.Division;

public interface IDivisionBuilder<out T> where T: IDivision
{
    IEnumerable<T> BuildDivisions();
}