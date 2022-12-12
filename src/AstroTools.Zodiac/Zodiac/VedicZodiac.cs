using AstroTools.Common.Model;
using AstroTools.Common.Model.Degree;
using AstroTools.Common.Repository;
using AstroTools.Zodiac.Builder;
using AstroTools.Zodiac.Model.CelestialObjects;
using AstroTools.Zodiac.Model.Divisions;

namespace AstroTools.Zodiac.Zodiac;

public class VedicZodiac : IZodiac
{
    private readonly IRepository<Sign> _signRepository;
    private readonly IRepository<Nakshatra> _nakshatraRepository;
    private readonly Dictionary<DegreeRange, IMappable> _degreeToSubLordMap;

    public VedicZodiac(
        IRepository<Sign> signRepository,
        IRepository<Nakshatra> nakshatraRepository)
    {
        _signRepository = signRepository;
        _nakshatraRepository = nakshatraRepository;

        _degreeToSubLordMap = _nakshatraRepository.GetAll()
            .SelectMany(n => n.SubDivisions[NakshatraSubDivisionBuilder.SubLords].Ranges)
            .ToDictionary(rng => rng.Range, rng => rng.Mapped);
    }

    public Sign GetSign(Degree degree) => _signRepository.Get(s => s.Region.Contains(degree)).First();
    public Nakshatra GetNakshatra(Degree degree) => _nakshatraRepository.Get(n => n.Region.Contains(degree)).First();
    public Planet GetSubLord(Degree degree) => (Planet)_degreeToSubLordMap.First(dts => dts.Key.Contains(degree)).Value;

    private string GetStatus(Planet planet, Sign sign)
    {
        return planet.GetType()
            .GetProperties()
            .First(prop => (prop.GetValue(planet) as SignEnum[] ?? Array.Empty<SignEnum>())
                .Contains(sign.SignEnum)).Name;
    }

    public IEnumerable<MappedData> Map(Planet planet, Degree degree)
    {
        var sign = GetSign(degree);
        var star = GetNakshatra(degree);
        var subLord = GetSubLord(degree);
        return new[]
        {
            new MappedData("Sign", sign, GetStatus(planet, sign)),
            new MappedData("SignLord", sign.Lord, GetStatus(sign.Lord, sign)),
            new MappedData("Star", star),
            new MappedData("StarLord", star.Lord, GetStatus(star.Lord, sign)),
            new MappedData("SubLord", subLord, GetStatus(subLord, sign))
        };
    }
}