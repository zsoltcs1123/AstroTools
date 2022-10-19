using EphemerisMapper.Model.Divisions;
using EphemerisMapper.Model.Enums;
using EphemerisMapper.Model.ZodiacPosition;

namespace EphemerisMapper.Service.Builder;

public class NakshatraBuilder : IDivisionBuilder<Nakshatra>
{
    private static readonly Degree Territory = new(13, 20, 0);
    
    private readonly Dictionary<SignEnum, DegreeRange> _signToCusp;


    public IEnumerable<Nakshatra> BuildDivisions()
    {
        throw new NotImplementedException();
    }
}