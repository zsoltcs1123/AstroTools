using AstroTools.Common.Factory;
using AstroTools.Common.Repository;
using AstroTools.Zodiac.Model.CelestialObjects;
using AstroTools.Zodiac.Model.Divisions;
using AstroTools.Zodiac.Model.Tarot;

namespace AstroTools.Zodiac.Builder;

public class SignSubDivisionBuilder : ISubDivisionBuilder<Sign>
{
    public const string Decans = "Decans";
    public const string Terms = "Terms";

    private readonly DecanBuilder _decanBuilder;
    private readonly TermBuilder _termBuilder;

    public SignSubDivisionBuilder(IRepository<TarotCard> tarotCardRepository, IRepository<Planet> planetFactory)
    {
        _decanBuilder = new DecanBuilder(tarotCardRepository);
        _termBuilder = new TermBuilder(planetFactory);
    }

    public Dictionary<string,SubDivision> BuildSubDivisions(Sign division)
    {
        return new Dictionary<string, SubDivision>
        {
            { "Decans", _decanBuilder.BuildDecans(division) },
            { "Terms", _termBuilder.BuildTerms(division) },
        };
    }
}