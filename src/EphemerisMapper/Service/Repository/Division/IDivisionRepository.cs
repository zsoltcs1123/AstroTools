using EphemerisMapper.Model.Divisions;
using EphemerisMapper.Model.Units;

namespace EphemerisMapper.Service.Repository.Division;

public interface IDivisionRepository<out T> where T : IDivision
{
    T Get(Degree degree);
    IEnumerable<T> GetAll();
}