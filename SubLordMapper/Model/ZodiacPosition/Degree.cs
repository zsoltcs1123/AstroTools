namespace SubLordMapper.Model.ZodiacPosition;

public record Degree
{
    public ZodiacalFormat Zodiacal { get; }
    public decimal Decimal { get; }

    public Degree(ZodiacalFormat zodiacal)
    {
        Zodiacal = zodiacal;
        Decimal = ConvertFromZodiacalToDecimal(zodiacal);
    }

    public Degree(int hours, int minutes, int seconds)
    {
        Zodiacal = new ZodiacalFormat(hours, minutes, seconds);
        Decimal = ConvertFromZodiacalToDecimal(Zodiacal);
    }

    public Degree(decimal @decimal)
    {
        Decimal = @decimal;
        Zodiacal = ConvertFromDecimalToZodiacal(@decimal);
    }

    public static bool operator <=(Degree degree1, Degree degree2)
    {
        return degree1.Decimal <= degree2.Decimal;
    }

    public static bool operator >=(Degree degree1, Degree degree2)
    {
        return degree1.Decimal > degree2.Decimal;
    }
    
    public static bool operator <(Degree degree1, Degree degree2)
    {
        return degree1.Decimal < degree2.Decimal;
    }
        
    public static bool operator >(Degree degree1, Degree degree2)
    {
        return degree1.Decimal > degree2.Decimal;
    }
    
    public static Degree operator +(Degree degree1, Degree deg)
    {
        return new Degree(degree1.Decimal + deg.Decimal);
    }
    
    public static Degree operator +(Degree degree1, decimal dec)
    {
        return new Degree(degree1.Decimal + dec);
    }

    private static decimal ConvertFromZodiacalToDecimal(ZodiacalFormat zodiacal)
    {
        return 0;
    }

    private static ZodiacalFormat ConvertFromDecimalToZodiacal(decimal @decimal)
    {
        return null;
    }
}