using CsvHelper;
using System.Globalization;

namespace EphemerisMapper.Service
{
    internal class EphemerisInitializer<T> : CsvDatainitializer<IEnumerable<T>, string>
    {
        public override List<T> Initialize(string fileName)
        {
            using var streamReader = File.OpenText(GetFullPath(fileName));
            using var csv = new CsvReader(streamReader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<T>();

            return records.ToList();
        }
    }
}