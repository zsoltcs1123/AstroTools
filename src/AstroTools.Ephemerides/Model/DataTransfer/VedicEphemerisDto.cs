namespace AstroTools.Ephemerides.Model.DataTransfer
{
    public record VedicEphemerisDto(
        DateTime Date,
        decimal Sun,
        decimal Mercury,
        decimal Venus,
        decimal Mars,
        decimal Jupiter,
        decimal Saturn,
        decimal MeanNode,
        decimal SouthNode) : EphemerisDto(Date);
}