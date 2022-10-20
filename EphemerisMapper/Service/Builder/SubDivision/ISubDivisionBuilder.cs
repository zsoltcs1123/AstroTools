using EphemerisMapper.Model.Divisions;

namespace EphemerisMapper.Service.Builder.SubDivision;

public interface ISubDivisionBuilder<in T> where T : IDivision
{
    IEnumerable<Model.SubDivisions.SubDivision> BuildSubDivisions(T division);
}