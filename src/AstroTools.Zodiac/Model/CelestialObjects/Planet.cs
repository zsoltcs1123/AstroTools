using AstroTools.Zodiac.Model.Enums;

namespace AstroTools.Zodiac.Model.CelestialObjects;

public record Planet() : ICelestialObject
{
    private const decimal VimshottariTotal = 120m;
    public PlanetEnum PlanetEnum { get; }
    public string Name { get; }
    public int VimShottariPeriod { get; }

    public SignEnum[] Lord { get; }
    public SignEnum[] Mitra { get; }
    public SignEnum[] Satru { get; }
    public SignEnum[] Sama { get; }
    public SignEnum[] Exalted { get; }
    public SignEnum[] Debilitated { get; }


    public Planet(PlanetEnum planetEnum, int vimShottariPeriod, SignEnum[] lord, SignEnum[] mitra, SignEnum[] satru,
        SignEnum[] sama, SignEnum[] exalted, SignEnum[] debilitated) : this()
    {
        Name = planetEnum.ToString();
        PlanetEnum = planetEnum;
        VimShottariPeriod = vimShottariPeriod;
        Lord = lord;
        Mitra = mitra;
        Satru = satru;
        Sama = sama;
        Exalted = exalted;
        Debilitated = debilitated;
    }

    public decimal VimShottariRatio => VimShottariPeriod / VimshottariTotal;
}