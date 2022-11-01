using AstroTools.Zodiac.Model.Divisions;

namespace AstroTools.Zodiac.Service.SubDivision;

public interface ISubDivisionBuilder<in T> where T : IDivision
{
    IEnumerable<Model.SubDivisions.SubDivision> BuildSubDivisions(T division);
}