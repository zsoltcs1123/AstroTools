using System.Globalization;
using AstroTools.Common.Model.Degree;
using CsvHelper;
using CsvHelper.Configuration;

namespace EphemerisMapperTest.Model;

public class DegreeTestData
{
    public static IEnumerable<object[]> GetDataFromCsv()
    {
        string file = $"{AppDomain.CurrentDomain.BaseDirectory}\\TestData\\moon_test.csv";

        var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
        {
            HasHeaderRecord = false
        };

        using var streamReader = File.OpenText(file);
        using var reader = new CsvReader(streamReader, csvConfig);

        while (reader.Read())
        {
            reader.TryGetField(1, out string dec);
            reader.TryGetField(2, out string zodiacal);
            yield return new object[] { StringToZodiacalFormat(zodiacal), Convert.ToDecimal(dec) };
        }
    }

    private static ZodiacalFormat StringToZodiacalFormat(string s)
    {
        //example: 03°Sc45'43''
        var deg = Convert.ToUInt32(s.Substring(0, 2));
        var min = Convert.ToUInt32(s.Substring(5, 2));
        var sec = Convert.ToUInt32(s.Substring(8, 2));
        return new ZodiacalFormat(ConvertTo360Deg(deg, s.Substring(3,2)), min, sec);
    }

    private static uint ConvertTo360Deg(uint deg, string sign) => deg + (uint)(sign switch
    {
        "Ar" => 0,
        "Ta" => 30,
        "Ge" => 60,
        "Cn" => 90,
        "Le" => 120,
        "Vi" => 150,
        "Li" => 180,
        "Sc" => 210,
        "Sg" => 240,
        "Cp" => 270,
        "Aq" => 300,
        "Pi" => 330,
        _ => 0
    });
}