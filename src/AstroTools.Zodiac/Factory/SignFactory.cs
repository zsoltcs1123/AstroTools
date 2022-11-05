using AstroTools.Common.Model.Degree;
using AstroTools.Common.Repository;
using AstroTools.Zodiac.Model.CelestialObjects;
using AstroTools.Zodiac.Model.Divisions;
using AstroTools.Zodiac.Model.Enums;
using AstroTools.Zodiac.Service.Cusp;
using AstroTools.Zodiac.Service.SubDivision;

namespace AstroTools.Zodiac.Factory;

public class SignFactory : DivisionFactory<Sign, SignEnum>
{
    private static readonly Degree Territory = new(30);

    private readonly Dictionary<SignEnum, DegreeRange> _signToCusp;

    public SignFactory(
        ICuspGenerator<SignEnum> cuspGenerator,
        IRepository<Planet> planetRepository,
        ISubDivisionBuilder<Sign> subDivisionBuilder)
        : base(Territory, cuspGenerator, planetRepository, subDivisionBuilder)
    {
        _signToCusp = cuspGenerator.GenerateCusps(Territory);
    }

    public override IEnumerable<Sign> CreateAll()
    {
        return Enum.GetValues<SignEnum>()
            .Select(s => new Sign(s.ToString(), s, _signToCusp[s], GetLord(s))).ToArray();
    }
}