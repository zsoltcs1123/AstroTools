using EphemerisMapper.Model.Enums;

namespace EphemerisMapper.Model.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class TraditionalLordAttribute : Attribute
{
    public PlanetEnum Lord { get; }

    public TraditionalLordAttribute(PlanetEnum lord)
    {
        Lord = lord;
    }
}