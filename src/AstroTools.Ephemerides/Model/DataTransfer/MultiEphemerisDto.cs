namespace AstroTools.Ephemeris.Model.DataTransfer
{
    public record MultiEphemerisDto(
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