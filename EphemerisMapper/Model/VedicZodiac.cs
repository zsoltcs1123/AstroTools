using EphemerisMapper.Model.CelestialObjects;
using EphemerisMapper.Model.Divisions;
using EphemerisMapper.Model.Enums;
using EphemerisMapper.Model.ZodiacPosition;
using EphemerisMapper.Service.Divisions;

namespace EphemerisMapper.Model;

public class VedicZodiac
{
    private static readonly Degree StarRegion = new Degree(13, 20, 0);

    private readonly Nakshatra[] _nakshatras = GenerateNakshatras();

    private static Nakshatra[] GenerateNakshatras() =>
        Enum.GetValues<StarEnum>()
            .Select(s => new Nakshatra(s)).ToArray();


    private readonly Dictionary<DegreeRange, Nakshatra> _degreeToNakshatraMap;
    private readonly Dictionary<DegreeRange, Planet> _degreeToSubLordMap;
    private readonly IDivisionProvider<Sign> _signProvider;

    public VedicZodiac(IDivisionProvider<Sign> signProvider)
    {
        _signProvider = signProvider;

        _degreeToNakshatraMap = _nakshatras.ToDictionary(n => n.Region, n => n);

        _degreeToSubLordMap = _nakshatras
            .SelectMany(n => n.SubLords.Ranges)
            .ToDictionary(rng => rng.Key, rng => new Planet(rng.Value));
    }

    public Sign GetSign(Degree degree) => _signProvider.Get(degree);
    public Nakshatra GetNakshatra(Degree degree) => _degreeToNakshatraMap.First(dts => dts.Key.Contains(degree)).Value;
    public Planet GetSubLord(Degree degree) => _degreeToSubLordMap.First(dts => dts.Key.Contains(degree)).Value;
}