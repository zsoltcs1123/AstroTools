namespace AstroTools.Ephemerides.Model.DataTransfer;

public record TropicalEphemerisDto(
    DateTime Date,
    decimal Sun,
    decimal Mercury,
    decimal Venus,
    decimal Mars,
    decimal Jupiter,
    decimal Saturn,
    decimal MeanNode,
    decimal SouthNode,
    decimal Uranus,
    decimal Neptune,
    decimal Pluto) : VedicEphemerisDto(Date, Sun, Mercury, Venus, Mars, Jupiter, Saturn, MeanNode, SouthNode);