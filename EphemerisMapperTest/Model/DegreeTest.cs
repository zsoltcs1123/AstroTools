using EphemerisMapper.Model.Units;

namespace EphemerisMapperTest.Model;

public class DegreeTest
{
    private static object[] _invalidConversionCases =
    {
        new object[] { new ZodiacalFormat(521, 0, 0) },
        new object[] { new ZodiacalFormat(50, 99, 0) },
        new object[] { new ZodiacalFormat(50, 0, 115) },
        new object[] { new ZodiacalFormat(50, 0, 0) },
    };

    private static decimal Rounder(decimal dec) => Math.Round(dec, 3);

    [TestCaseSource(typeof(DegreeTestData), nameof(DegreeTestData.GetDataFromCsv))]
    public void ConstructionFromZodiacalTest(ZodiacalFormat zodiacal, decimal expected)
    {
        var degree = new Degree(zodiacal);
        Assert.That(Rounder(degree.Dec), Is.EqualTo(Rounder(expected)));
    }

    [TestCaseSource(nameof(_invalidConversionCases))]
    public void ConstructionFromInvalidZodiacalTest(ZodiacalFormat zodiacal)
    {
        Assert.Throws<Exception>(() => { new Degree(zodiacal); });
    }


    [TestCaseSource(typeof(DegreeTestData), nameof(DegreeTestData.GetDataFromCsv))]
    public void ConstructionFromDecimalTest(ZodiacalFormat expected, decimal dec)
    {
        var degree = new Degree(dec);
        Assert.That(degree.Zodiacal, Is.EqualTo(expected));
    }
}