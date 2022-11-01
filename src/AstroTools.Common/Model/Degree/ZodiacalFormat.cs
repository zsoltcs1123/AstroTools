namespace AstroTools.Common.Model.Degree;

public record ZodiacalFormat(uint Degrees, uint Minutes, uint Seconds)
{
    public override string ToString()
    {
        return $"{Degrees}, {Minutes}, {Seconds}";
    }
}