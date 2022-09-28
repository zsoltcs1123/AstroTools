namespace SubLordMapper.Model
{
    internal class Ephemerides
    {
        private readonly List<Ephemeris> _entries = new();
        private readonly List<SubLord> _subLordTable = new();

        private Dictionary<DateTime, Dictionary<Planet, (double Longitude, SubLord? SubLord)>> _subLordMapping = new();

        public Ephemerides(IEnumerable<Ephemeris> entries)
        {
            _entries = entries.ToList();
        }

        public IEnumerable<EphemerisForPlanet> GetSubLordMapping(DateTime start, Planet planet, DateTime? end = null)
        {
            return _subLordMapping
                .Where(kvp => kvp.Key >= start && end.HasValue ? kvp.Key <= end : true)
                .Select(kvp => new EphemerisForPlanet(kvp.Key, kvp.Value[planet].Longitude, kvp.Value[planet].SubLord));
        }

        public void Add(Ephemeris ephemerida)
        {
            _entries.Add(ephemerida);
        }

        public void Add(IEnumerable<Ephemeris> entries)
        {
            _entries.AddRange(entries);
        }

        public void AddSubLordTable(IEnumerable<SubLord> subLords)
        {
            _subLordTable.Clear();
            _subLordTable.AddRange(subLords);

            _subLordMapping = _entries.ToDictionary(e => e.Date, e => e.GenerateSubLords(_subLordTable));
        }
    }
}