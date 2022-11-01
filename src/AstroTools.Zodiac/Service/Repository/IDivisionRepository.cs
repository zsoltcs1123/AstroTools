using AstroTools.Common.Model.Degree;
using AstroTools.Zodiac.Model.Divisions;

namespace AstroTools.Zodiac.Service.Repository;

public interface IDivisionRepository<out T> where T : IDivision
{
    T Get(Degree degree);
    IEnumerable<T> GetAll();
}