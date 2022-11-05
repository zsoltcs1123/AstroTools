namespace AstroTools.Common.Extensions;

public static class DateTimeExtensions
{
    public static bool IsBetween(this DateTime date, DateTime? start, DateTime? end)
    {
        return start <= date && date <= end;
    }
}