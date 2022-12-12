namespace AstroTools.Zodiac.Model.Tarot;

public record CourtCard(Character Character, Suit Suit) : TarotCard
{
    public override string Name => $"{Character} of {Suit}";
}