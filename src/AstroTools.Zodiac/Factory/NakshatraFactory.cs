using AstroTools.Common.Model.Degree;
using AstroTools.Common.Repository;
using AstroTools.Zodiac.Model.CelestialObjects;
using AstroTools.Zodiac.Model.Divisions;
using AstroTools.Zodiac.Model.Enums;
using AstroTools.Zodiac.Service.Cusp;
using AstroTools.Zodiac.Service.SubDivision;

namespace AstroTools.Zodiac.Factory;

public class NakshatraFactory : DivisionFactory<Nakshatra, StarEnum>
{
    private static readonly Degree Territory = new Degree(13, 20, 0);
    
    public NakshatraFactory(
        ICuspGenerator<StarEnum> cuspGenerator,
        IRepository<Planet> planetRepository,
        ISubDivisionBuilder<Nakshatra> subDivisionBuilder) 
        : base(Territory, cuspGenerator, planetRepository, subDivisionBuilder)
    {
        
    }

    public override IEnumerable<Nakshatra> CreateAll()
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