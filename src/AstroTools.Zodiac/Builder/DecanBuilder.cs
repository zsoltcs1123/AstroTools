using AstroTools.Common.Factory;
using AstroTools.Common.Model.Degree;
using AstroTools.Common.Repository;
using AstroTools.Zodiac.Model.Divisions;
using AstroTools.Zodiac.Model.Tarot;

namespace AstroTools.Zodiac.Builder;

public class DecanBuilder
{
    private readonly Dictionary<DegreeRange, MinorCard> _cards;

    public DecanBuilder(IRepository<TarotCard> tarotCardRepository)
    {
        _cards = tarotCardRepository
            .GetAll()
            .OfType<MinorCard>()
            .Where(m => m.RangeInZodiac is not null)
            .ToDictionary(m => m.RangeInZodiac, m => m);
    }

    public SubDivision BuildDecans(Sign sign)
    {
        var ranges = new List<SubDivisionRange>();
        for (var i = sign.Region.Start.Zodiacal.Degrees; i < sign.Region.End.Zodiacal.Degrees; i += 10)
        {
            var degreeRange = new DegreeRange(new Degree(i), new Degree(i + 10));
            ranges.Add(new SubDivisionRange(degreeRange, _cards[degreeRange]));
        }

        return new SubDivision("Decans", ranges);
    }
}