using AstroTools.Zodiac.Attributes;
using AstroTools.Zodiac.Model.CelestialObjects;

namespace AstroTools.Zodiac.Model.Divisions;

public enum SignEnum
{
    [TraditionalLord(PlanetEnum.Mars)]
    Aries,
    [TraditionalLord(PlanetEnum.Venus)]
    Taurus,
    [TraditionalLord(PlanetEnum.Mercury)]
    Gemini,
    [TraditionalLord(PlanetEnum.Moon)]
    Cancer,
    [TraditionalLord(PlanetEnum.Sun)]
    Leo, 
    [TraditionalLord(PlanetEnum.Mercury)]
    Virgo,
    [TraditionalLord(PlanetEnum.Venus)]
    Libra,
    [TraditionalLord(PlanetEnum.Mars)]
    Scorpio,
    [TraditionalLord(PlanetEnum.Jupiter)]
    Saggitarius,
    [TraditionalLord(PlanetEnum.Saturn)]
    Capricorn,
    [TraditionalLord(PlanetEnum.Saturn)]
    Aquarius,
    [TraditionalLord(PlanetEnum.Jupiter)]
    Pisces
}