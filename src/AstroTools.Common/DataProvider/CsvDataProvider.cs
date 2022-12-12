using System.Globalization;
using CsvHelper;

namespace AstroTools.Common.DataProvider
{
    public class CsvDataProvider<T> : IDataProvider<IEnumerable<T>, string>
    {
        private static string GetFullPath(string fileName) => $"{AppDomain.CurrentDomain.BaseDirectory}\\{fileName}";

        public IEnumerable<T> Provide(string fileName)
        {
            using var streamReader = File.OpenText(GetFullPath(fileName));
            using var csv = new CsvReader(streamReader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<T>();

            return records.ToList();
        }
    }
}