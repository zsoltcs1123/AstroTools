using AstroTools.Common.Factory;
using AstroTools.Common.Model.Degree;
using AstroTools.Zodiac.Model.CelestialObjects;
using AstroTools.Zodiac.Model.Divisions;
using AstroTools.Zodiac.Model.Tarot;

namespace AstroTools.Zodiac.Factory;

public class TarotCardFactory : IFactory<TarotCard>
{
    public IEnumerable<TarotCard> CreateAll()
    {
        return CreateMinorCards().Concat(CreateCourtCards()).Concat(CreateMajorCards());
    }

    private static IEnumerable<TarotCard> CreateCourtCards()
    {
        foreach (var suit in Enum.GetValues<Suit>())
        {
            foreach (var character in Enum.GetValues<Character>())
            {
                yield return new CourtCard(character, suit);
            }
        }
    }

    private IEnumerable<TarotCard> CreateMajorCards()
    {
        for (int i = 0; i < 21; i++)
        {
            yield return new MajorCard(i, GetMajorCardTheme(i));
        }
    }

    private static string? GetMajorCardTheme(int number)
    {
        return number switch
        {
            0 => "The Fool",
            1 => "The Juggler",
            2 => "The High Priestess",
            3 => "The Empress",
            4 => "The Emperor",
            5 => "The Hierophant",
            6 => "The Lovers",
            7 => "The Chariot",
            8 => "Adjustment",
            9 => "The Hermit",
            10 => "Fortune",
            11 => "Lust",
            12 => "The Hanged Man",
            13 => "Death",
            14 => "Art",
            15 => "The Devil",
            16 => "The Tower",
            17 => "The Star",
            18 => "The Moon",
            20 => "The Aeon",
            21 => "The World",
            _ => null
        };
    }

    private IEnumerable<MinorCard> CreateMinorCards()
    {
        foreach (var suit in Enum.GetValues<Suit>())
        {
            for (int i = 1; i <= 10; i++)
            {
                var planetAndSign = GetPlanetAndSignForCard(suit, i);
                yield return new MinorCard(suit, i, planetAndSign.Planet, planetAndSign.Sign,
                    planetAndSign.ZodiacRange);
            }
        }
    }

    private (PlanetEnum? Planet, SignEnum? Sign, DegreeRange? ZodiacRange) GetPlanetAndSignForCard(Suit suit,
        int number)
    {
        return suit switch
        {
            Suit.Wands => number switch
            {
                1 => (null, null, null),
                2 => (PlanetEnum.Mars, SignEnum.Aries, new DegreeRange(0, 10)),
                3 => (PlanetEnum.Sun, SignEnum.Aries, new DegreeRange(10, 20)),
                4 => (PlanetEnum.Venus, SignEnum.Aries, new DegreeRange(20, 30)),
                5 => (PlanetEnum.Saturn, SignEnum.Leo, new DegreeRange(120, 130)),
                6 => (PlanetEnum.Jupiter, SignEnum.Leo, new DegreeRange(130, 140)),
                7 => (PlanetEnum.Mars, SignEnum.Leo, new DegreeRange(140, 150)),
                8 => (PlanetEnum.Mercury, SignEnum.Saggitarius, new DegreeRange(240, 250)),
                9 => (PlanetEnum.Sun, SignEnum.Saggitarius, new DegreeRange(250, 260)),
                10 => (PlanetEnum.Saturn, SignEnum.Saggitarius, new DegreeRange(260, 270)),
                _ => (null, null, null)
            },
            Suit.Cups => number switch
            {
                1 => (null, null, null),
                2 => (PlanetEnum.Venus, SignEnum.Cancer, new DegreeRange(90, 100)),
                3 => (PlanetEnum.Mercury, SignEnum.Cancer, new DegreeRange(100, 110)),
                4 => (PlanetEnum.Moon, SignEnum.Cancer, new DegreeRange(110, 120)),
                5 => (PlanetEnum.Mars, SignEnum.Scorpio, new DegreeRange(210, 220)),
                6 => (PlanetEnum.Sun, SignEnum.Scorpio, new DegreeRange(220, 230)),
                7 => (PlanetEnum.Venus, SignEnum.Scorpio, new DegreeRange(230, 240)),
                8 => (PlanetEnum.Saturn, SignEnum.Pisces, new DegreeRange(330, 340)),
                9 => (PlanetEnum.Jupiter, SignEnum.Pisces, new DegreeRange(340, 350)),
                10 => (PlanetEnum.Mars, SignEnum.Pisces, new DegreeRange(350, 360)),
                _ => (null, null, null)
            },
            Suit.Swords => number switch
            {
                1 => (null, null, null),
                2 => (PlanetEnum.Venus, SignEnum.Libra, new DegreeRange(180, 190)),
                3 => (PlanetEnum.Mercury, SignEnum.Libra, new DegreeRange(190, 200)),
                4 => (PlanetEnum.Moon, SignEnum.Libra, new DegreeRange(200, 210)),
                5 => (PlanetEnum.Jupiter, SignEnum.Aquarius, new DegreeRange(300, 310)),
                6 => (PlanetEnum.Mars, SignEnum.Aquarius, new DegreeRange(310, 320)),
                7 => (PlanetEnum.Sun, SignEnum.Aquarius, new DegreeRange(320, 330)),
                8 => (PlanetEnum.Moon, SignEnum.Gemini, new DegreeRange(60, 70)),
                9 => (PlanetEnum.Saturn, SignEnum.Gemini, new DegreeRange(70, 80)),
                10 => (PlanetEnum.Jupiter, SignEnum.Gemini, new DegreeRange(80, 90)),
                _ => (null, null, null)
            },
            Suit.Pentacles => number switch
            {
                1 => (null, null, null),
                2 => (PlanetEnum.Sun, SignEnum.Capricorn, new DegreeRange(270, 280)),
                3 => (PlanetEnum.Venus, SignEnum.Capricorn, new DegreeRange(280, 290)),
                4 => (PlanetEnum.Mercury, SignEnum.Capricorn, new DegreeRange(290, 300)),
                5 => (PlanetEnum.Jupiter, SignEnum.Taurus, new DegreeRange(30, 40)),
                6 => (PlanetEnum.Mars, SignEnum.Taurus, new DegreeRange(40, 50)),
                7 => (PlanetEnum.Sun, SignEnum.Taurus, new DegreeRange(50, 60)),
                8 => (PlanetEnum.Mercury, SignEnum.Virgo, new DegreeRange(150, 160)),
                9 => (PlanetEnum.Moon, SignEnum.Virgo, new DegreeRange(160, 170)),
                10 => (PlanetEnum.Saturn, SignEnum.Virgo, new DegreeRange(170, 180)),
                _ => (null, null, null)
            },
            _ => (null, null, null)
        };
    }
}