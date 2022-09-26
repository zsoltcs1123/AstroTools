namespace SubLordMapper.Model
{
    internal class Ephemerides
    {
        private readonly List<Ephemeris> _entries = new();
        private readonly List<SubLord> _subLords = new();
        private readonly List<EphemerisWithSubLord> _subLordMapping = new();

        public Ephemerides(IEnumerable<Ephemeris> entries)
        {
            _entries = entries.ToList();
        }

        public IEnumerable<EphemerisForPlanet> GetSubLordMapping(DateTime start, Planet planet, DateTime? end = null)
        {
            return _subLordMapping.Where(s => s.Entry.Date >= start && end.HasValue ? s.Entry.Date <= end : true)
                .Select(s => new EphemerisForPlanet(s.Entry.Date, GetLongitude(planet, s.Entry), GetSubLord(planet, s)));
        }

        private IEnumerable<EphemerisWithSubLord> GetSubLordMapping()
        {
            return _entries.Select(e => new EphemerisWithSubLord(e,
                MapSubLord(e.Sun),
                MapSubLord(e.Mercury),
                MapSubLord(e.Venus),
                MapSubLord(e.Mars),
                MapSubLord(e.Jupiter),
                MapSubLord(e.Saturn),
                MapSubLord(e.MeanNode),
                MapSubLord(e.SouthNode)));
        }

        private SubLord MapSubLord(double longitude)
        {
            return _subLords.FirstOrDefault(s => longitude > s.Degrees.Start && longitude < s.Degrees.End)
                ?? new SubLord(0,"","","","","", new DegreeRange(0,0));
        }

        private SubLord GetSubLord(Planet planet, EphemerisWithSubLord ephemeris)
        {
            return planet switch
            {
                Planet.Sun => ephemeris.Sun,
                Planet.Mercury => ephemeris.Mercury,
                Planet.Venus => ephemeris.Venus,
                Planet.Mars => ephemeris.Mars,
                Planet.Jupiter => ephemeris.Jupiter,
                Planet.Saturn => ephemeris.Saturn,
                Planet.Rahu => ephemeris.Rahu,
                Planet.Ketu => ephemeris.Ketu,
                _ => throw new NotImplementedException()
            };
        }

        private double GetLongitude(Planet planet, Ephemeris ephemeris)
        {
            return planet switch
            {
                Planet.Sun => ephemeris.Sun,
                Planet.Mercury => ephemeris.Mercury,
                Planet.Venus => ephemeris.Venus,
                Planet.Mars => ephemeris.Mars,
                Planet.Jupiter => ephemeris.Jupiter,
                Planet.Saturn => ephemeris.Saturn,
                Planet.Rahu => ephemeris.MeanNode,
                Planet.Ketu => ephemeris.SouthNode,
                _ => throw new NotImplementedException()
            };
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
            _subLords.Clear();
            _subLordMapping.Clear();

            _subLords.AddRange(subLords);
            _subLordMapping.AddRange(GetSubLordMapping());
        }
    }
}
