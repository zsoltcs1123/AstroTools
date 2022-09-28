namespace SubLordMapper.Model.DataTransfer
{
    public record MultiEphemerisDto(DateTime Date, double Sun, double Mercury, double Venus, double Mars, double Jupiter, double Saturn, double MeanNode, double SouthNode);

}
