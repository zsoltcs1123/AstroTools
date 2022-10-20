namespace EphemerisMapper.Model.Units;

public record Degree
{
    public ZodiacalFormat Zodiacal { get; }
    public decimal Dec { get; }

    public Degree(ZodiacalFormat zodiacal)
    {
        Zodiacal = zodiacal;
        Dec = ConvertFromZodiacalToDecimal(zodiacal);
    }

    public Degree(uint degrees, uint minutes, uint seconds)
    {
        if (degrees > 360 || minutes > 60 || seconds > 60)
        {
            throw new Exception($"Invalid degree values in : {degrees}, {minutes}, {seconds}");
        }

        Zodiacal = new ZodiacalFormat(degrees, minutes, seconds);
        Dec = ConvertFromZodiacalToDecimal(Zodiacal);
    }

    public Degree(decimal dec)
    {
        if (dec is > 361 or < 0)
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
    
    public Degree RoundToNearestWhole()
    {
        uint AddOne(uint u) => u % 10 == 9 ? u + 1 : u;
        uint SixtyToZero(uint u) => u == 60 ? 0 : u;
        // uint ThreeSixtyToZero(uint u) => u == 360 ? 0 : u;

        var deg = AddOne(Zodiacal.Degrees);
        var min = SixtyToZero(AddOne(Zodiacal.Minutes));
        var sec = SixtyToZero(AddOne(Zodiacal.Seconds));

        return new Degree(deg, min, sec);
    }

    private static decimal ConvertFromZodiacalToDecimal(ZodiacalFormat zodiacal)
    {
        decimal sec = zodiacal.Seconds == 0 ? 0 : 60m / zodiacal.Seconds;
        decimal min = sec == 0 ? zodiacal.Minutes : zodiacal.Minutes + 1m / sec;
        var degree = min == 0 ? zodiacal.Degrees : zodiacal.Degrees + 1m / (60m / min);
        return degree;
    }

    private static ZodiacalFormat ConvertFromDecimalToZodiacal(decimal dec)
    {
        var decimalPart = GetDecimalPart(dec);

        var minute = decimalPart * 60;
        var minuteDecimalPart = GetDecimalPart(minute);

        var second = minuteDecimalPart * 60;

        return new ZodiacalFormat(
            GetIntegralPart(dec),
            GetIntegralPart(minute),
            GetIntegralPart(second));
    }

    private static decimal GetDecimalPart(decimal dec) => dec - Math.Truncate(dec);
    private static uint GetIntegralPart(decimal dec) => (uint)Math.Truncate(dec);

    public override string ToString()
    {
        return $"{nameof(Zodiacal)}: [{Zodiacal}], {nameof(Dec)}: [{Dec.ToString("0.0000")}]";
    }
}