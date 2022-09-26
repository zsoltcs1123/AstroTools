using CsvHelper;
using SubLordMapper.Model;
using System.Globalization;

namespace SubLordMapper.Service
{
    internal class EphemerisInitializer : CsvDatainitializer<Ephemerides, string> 
    {
        public override Ephemerides Initialize(string fileName)
        {
            using var streamReader = File.OpenText(GetFullPath(fileName));
            using (var csv = new CsvReader(streamReader, CultureInfo.InvariantCulture))
            {
                //csv.Context.RegisterClassMap<EphemerisEntryMap>();
                var records = csv.GetRecords<Ephemeris>();

                return new Ephemerides(records);
            }
        }

    }
}
