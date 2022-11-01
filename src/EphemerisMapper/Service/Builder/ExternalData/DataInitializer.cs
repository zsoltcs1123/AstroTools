namespace EphemerisMapper.Service.Builder.ExternalData
{
    internal abstract class CsvDatainitializer<T, Req> : IDataInitializer<T, string>
    {
        protected readonly static string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;

        protected static string GetFullPath(string fileName) => $"{AppDomain.CurrentDomain.BaseDirectory}\\{fileName}";

        public abstract T Initialize(string fileName);

    }
}
