namespace EphemerisMapper.Model.ZodiacPosition;

public record Degree
{
    public ZodiacalFormat Zodiacal { get; }
    public decimal Dec { get; }

    public Degree(ZodiacalFormat zodiacal)
    {
        Zodiacal = zodiacal;
        Dec = ConvertFromZodiacalToDecimal(zodiacal);
    }

    public Degree(uint degrees, uint hours, uint minutes, uint seconds)
    {
        if (degrees > 360 || hours > 60 || minutes > 60 || seconds > 60)
        {
            throw new Exception($"Invalid degree values in : {degrees}, {hours}, {minutes}, {seconds}");
        }
        
        Zodiacal = new ZodiacalFormat(degrees, hours, minutes, seconds);
        Dec = ConvertFromZodiacalToDecimal(Zodiacal);
    }

    public Degree(decimal dec)
    {
        if (dec is > 360 or < 0 )
        {
            throw new Exception($"Invalid degree value: {dec}");
        }
        
        Dec = dec;
        Zodiacal = ConvertFromDecimalToZodiacal(dec);
    }

    public static bool operator <=(Degree degree1, Degree degree2)
    {
        return degree1.Dec <= degree2.Dec;
    }

    public static bool operator >=(Degree degree1, Degree degree2)
    {
        return degree1.Dec > degree2.Dec;
    }

    public static bool operator <(Degree degree1, Degree degree2)
    {
        return degree1.Dec < degree2.Dec;
    }

    public static bool operator >(Degree degree1, Degree degree2)
    {
        return degree1.Dec > degree2.Dec;
    }

    public static Degree operator +(Degree degree1, Degree deg)
    {
        return new Degree(degree1.Dec + deg.Dec);
    }

    public static Degree operator +(Degree degree1, decimal dec)
    {
        return new Degree(degree1.Dec + dec);
    }

    private static decimal ConvertFromZodiacalToDecimal(ZodiacalFormat zodiacal)
    {
        decimal sec = zodiacal.Seconds == 0 ? 0 : 60 / zodiacal.Seconds;
        decimal min = sec == 0 ? zodiacal.Minutes : zodiacal.Minutes + (1 / sec);
        decimal hour = min == 0 ? zodiacal.Hours : zodiacal.Hours + (1 / (60/ min));
        var degree = hour == 0 ? zodiacal.Degrees : zodiacal.Degrees + 1 / (60 / hour);
        return degree;
    }

    private static ZodiacalFormat ConvertFromDecimalToZodiacal(decimal dec)
    {
        var decimalPart = GetDecimalPart(dec);

        var hour = 60 / (1 / decimalPart);
        var hourDecimalPart = GetDecimalPart(hour);

        var minute = 60 / (1 / hourDecimalPart);
        var minuteDecimalPart = GetDecimalPart(minute);

        var second = 60 / (1 / minuteDecimalPart);

        return new ZodiacalFormat(
            GetIntegralPart(dec),
            GetIntegralPart(hour),
            GetIntegralPart(minute),
            GetIntegralPart(second));
    }

    private static decimal GetDecimalPart(decimal dec) => dec - Math.Truncate(dec);
    private static uint GetIntegralPart(decimal dec) => (uint)Math.Truncate(dec);
}