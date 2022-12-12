using AstroTools.Common.Model;
using AstroTools.Common.Model.Degree;
using AstroTools.Common.Repository;
using AstroTools.Zodiac.Builder;
using AstroTools.Zodiac.Model.CelestialObjects;
using AstroTools.Zodiac.Model.Divisions;

namespace AstroTools.Zodiac.Zodiac;

public class TropicalZodiac : IZodiac
{
    private readonly IRepository<Sign> _signRepository;

    public TropicalZodiac(IRepository<Sign> signRepository)
    {
        _signRepository = signRepository;
    }

    public Sign GetSign(Degree degree) => _signRepository.Get(s => s.Region.Contains(degree)).First();

    public static IMappable GetMappable(SubDivision subDivision, Degree degree) =>
        subDivision.Ranges.First(r => r.Range.Contains(degree)).Mapped;

    public IEnumerable<MappedData> Map(Planet planet, Degree degree)
    {
        var sign = GetSign(degree);
        var decans = sign.SubDivisions[SignSubDivisionBuilder.Decans];
        var terms = sign.SubDivisions[SignSubDivisionBuilder.Terms];
        
        return new[]
        {
            new MappedData("Sign", sign),
            new MappedData("Decan", GetMappable(decans, degree)),
            new MappedData("Term", GetMappable(terms, degree))
        };
    }
}