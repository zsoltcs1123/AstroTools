using EphemerisMapper.Model.Divisions;

namespace EphemerisMapper.Service.Builder;

public interface ISubDivisionBuilder<T> where T : IDivision
{
    IEnumerable<SubDivision> BuildSubDivisions();
}