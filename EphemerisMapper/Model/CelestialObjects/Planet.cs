using EphemerisMapper.Model.Enums;

namespace EphemerisMapper.Model.CelestialObjects;

public record Planet() : ICelestialObject
{
    public PlanetEnum PlanetEnum { get; }
    public string Name { get; }
    public int VimShottariPeriod { get; }
    
    public Planet(PlanetEnum planetEnum, int vimShottariPeriod) : this()
    {
        Name = planetEnum.ToString();
        VimShottariPeriod = vimShottariPeriod;
    }
}