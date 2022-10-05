using EphemerisMapper.Model.ZodiacPosition;

namespace EphemerisMapperTest.Model;

public class DegreeTest
{
    #region Zodiacal
    
    private static object[] _convertFromZodiacalToDecimalCases =
    {
        new object[] { new ZodiacalFormat(0, 0, 0, 0), 0m },
        new object[] { new ZodiacalFormat(0, 46, 40, 20), 0.7779m },
        new object[] { new ZodiacalFormat(120, 33, 21, 12), 120.5559m },
        new object[] { new ZodiacalFormat(359, 21, 12, 18), 359.3534m },
        new object[] { new ZodiacalFormat(150, 0, 11, 12), 150.0031m },
        new object[] { new ZodiacalFormat(241, 22, 0, 1), 241.3667m },
        new object[] { new ZodiacalFormat(66, 19, 1, 0), 66.3169m },
    };

    private static object[] _invalidConvertFromZodiacalToDecimalCases =
    {
        new object[] { new ZodiacalFormat(521, 0, 0, 0) },
        new object[] { new ZodiacalFormat(50, 99, 0, 0) },
        new object[] { new ZodiacalFormat(50, 0, 115, 0) },
        new object[] { new ZodiacalFormat(50, 0, 0, 199) },
    };

    private static decimal Rounder(decimal dec) => Math.Round(dec, 4);

    [TestCaseSource(nameof(_convertFromZodiacalToDecimalCases))]
    public void ConstructionFromZodiacalTest(ZodiacalFormat zodiacal, decimal expected)
    {
        var degree = new Degree(zodiacal);
        Assert.That(Rounder(degree.Dec), Is.EqualTo(expected));
    }

    [TestCaseSource(nameof(_invalidConvertFromZodiacalToDecimalCases))]
    public void ConstructionFromInvalidZodiacalTest(ZodiacalFormat zodiacal)
    {
        Assert.Throws<Exception>(() =>
        {
            new Degree(zodiacal);
        });
    }
    
    #endregion
    
    #region Decimal
    
    
    public void ConstructionFromDecimalTest(ZodiacalFormat zodiacal, decimal expected)
    {
        var degree = new Degree(zodiacal);
        Assert.That(Rounder(degree.Dec), Is.EqualTo(expected));
    }
    
    #endregion
}