using EphemerisMapper.Model;
using EphemerisMapper.Model.CelestialObjects;
using EphemerisMapper.Model.Divisions;
using EphemerisMapper.Model.Units;
using EphemerisMapper.Service.Repository.Division;

namespace EphemerisMapper.Service.Zodiac;

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

    public Dictionary<string, IMappable> Map(Degree degree)
    {
        var sign = GetSign(degree);
        var star = GetNakshatra(degree);
        return new Dictionary<string, IMappable>()
        {
            { "Sign", sign },
            { "SignLord", sign.Lord },
            { "Star", star },
            { "StarLord", star.Lord },
            { "SubLord", GetSubLord(degree) },
        };
    }
}