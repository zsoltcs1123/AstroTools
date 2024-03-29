﻿using AstroTools.Zodiac.Model.CelestialObjects;

namespace AstroTools.Zodiac.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class TraditionalLordAttribute : Attribute
{
    public PlanetEnum Lord { get; }

    public TraditionalLordAttribute(PlanetEnum lord)
    {
        Lord = lord;
    }
}