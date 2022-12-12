using AstroTools.Common.Model.Degree;
using AstroTools.Zodiac.Model.CelestialObjects;
using AstroTools.Zodiac.Model.Divisions;

namespace AstroTools.Zodiac.Model.Tarot;

public record MinorCard(Suit Suit, int Number, PlanetEnum? Planet, SignEnum? Sign, DegreeRange? RangeInZodiac) : TarotCard
{
    public override string Name => $"{Number} of {Suit}";
}