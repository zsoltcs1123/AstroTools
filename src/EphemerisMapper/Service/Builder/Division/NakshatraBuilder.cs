using EphemerisMapper.Model.CelestialObjects;
using EphemerisMapper.Model.Divisions;
using EphemerisMapper.Model.Enums;
using EphemerisMapper.Model.Units;
using EphemerisMapper.Service.Builder.Cusp;
using EphemerisMapper.Service.Builder.SubDivision;
using EphemerisMapper.Service.Repository;
using EphemerisMapper.Service.Repository.CelestialObjects;

namespace EphemerisMapper.Service.Builder.Division;

public class NakshatraBuilder : DivisionBuilder<Nakshatra, StarEnum>
{
    private static readonly Degree Territory = new Degree(13, 20, 0);
    
    public NakshatraBuilder(
        ICuspGenerator<StarEnum> cuspGenerator,
        ICelestialObjectRepository<Planet> planetRepository,
        ISubDivisionBuilder<Nakshatra> subDivisionBuilder) 
        : base(Territory, cuspGenerator, planetRepository, subDivisionBuilder)
    {
        
    }

    public override IEnumerable<Nakshatra> BuildDivisions()
    {
        var nakshatras = Enum.GetValues<StarEnum>()
            .Select(s => new Nakshatra(s, DivisionToCusp[s], GetLord(s))).ToArray();

        foreach (var nakshatra in nakshatras)
        {
            nakshatra.SubDivisions.AddRange(SubDivisionBuilder.BuildSubDivisions(nakshatra));
        }

        return nakshatras;
    }
}