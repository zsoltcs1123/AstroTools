using AstroTools.Common.Model;

namespace AstroTools.Zodiac.Model.Tarot;

public abstract record TarotCard : IMappable
{
    public abstract string Name { get;  }
}