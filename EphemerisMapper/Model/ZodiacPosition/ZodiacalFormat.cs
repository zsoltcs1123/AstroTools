namespace EphemerisMapper.Model.ZodiacPosition;

public record ZodiacalFormat(uint Degrees, uint Minutes, uint Seconds)
{
    public override string ToString()
    {
        return $"{Degrees}, {Minutes}, {Seconds}";
    }
}