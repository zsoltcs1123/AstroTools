namespace EphemerisMapper.Model.ZodiacPosition;

public static class DegreeExtensions
{
    public static Degree RoundToNearestWhole(this Degree degree)
    {
        uint AddOne(uint u) => u % 10 == 9 ? u + 1 : u;
        uint SixtyToZero(uint u) => u == 60 ? 0 : u;
        uint ThreeSixtyToZero(uint u) => u == 60 ? 0 : u;

        var deg = ThreeSixtyToZero(degree.Zodiacal.Degrees);
        var min = SixtyToZero(AddOne(degree.Zodiacal.Minutes));
        var sec = SixtyToZero(AddOne(degree.Zodiacal.Seconds));

        return new Degree(deg, min, sec);
    }
}