using EphemerisMapper.Model.Enums;

namespace EphemerisMapper.Model.CelestialObjects;

public record Planet() : ICelestialObject
{
    private const decimal VimshottariTotal = 120m;
    public PlanetEnum PlanetEnum { get; }
    public string Name { get; }
    public int VimShottariPeriod { get; }

    public Planet(PlanetEnum planetEnum, int vimShottariPeriod) : this()
    {
        Name = planetEnum.ToString();
        PlanetEnum = planetEnum;
        VimShottariPeriod = vimShottariPeriod;
    }

    public decimal VimShottariRatio => VimShottariPeriod / VimshottariTotal;
}