using AstroTools.Zodiac.Model.Divisions;

namespace AstroTools.Zodiac.Builder;

public interface ISubDivisionBuilder<in T> where T : IDivision
{
    Dictionary<string, SubDivision> BuildSubDivisions(T division);
}