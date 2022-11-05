using AstroTools.Common.Model;
using AstroTools.Common.Model.Degree;
using AstroTools.Zodiac.Model.CelestialObjects;
using AstroTools.Zodiac.Model.Divisions;
using AstroTools.Zodiac.Model.Enums;
using AstroTools.Zodiac.Service.Repository;

namespace AstroTools.Zodiac.Service;

public class VedicZodiac : IZodiac
{
    private readonly IDivisionRepository<Sign> _signRepository;
    private readonly IDivisionRepository<Nakshatra> _nakshatraRepository;
    private readonly Dictionary<DegreeRange, Planet> _degreeToSubLordMap;

    public VedicZodiac(
        IDivisionRepository<Sign> signRepository,
        IDivisionRepository<Nakshatra> nakshatraRepository)
    {
        _signRepository = signRepository;
        _nakshatraRepository = nakshatraRepository;

        _degreeToSubLordMap = _nakshatraRepository.GetAll()
            .SelectMany(n => n.SubDivisions[0].Ranges)
            .ToDictionary(rng => rng.Range, rng => rng.Lord);
    }

    public Sign GetSign(Degree degree) => _signRepository.Get(degree);
    public Nakshatra GetNakshatra(Degree degree) => _nakshatraRepository.Get(degree);
    public Planet GetSubLord(Degree degree) => _degreeToSubLordMap.First(dts => dts.Key.Contains(degree)).Value;

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