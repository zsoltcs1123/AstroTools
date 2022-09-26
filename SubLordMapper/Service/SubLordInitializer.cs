using SubLordMapper.Model;

namespace SubLordMapper.Service
{
    internal class SubLordInitializer : CsvDatainitializer<IEnumerable<SubLord>, string>
    {
        public override IEnumerable<SubLord> Initialize(string filename)
        {
            string[] lines = File.ReadAllLines(GetFullPath(filename));

            List<SubLord> ret = new();

            for (int i = 1; i < lines.Length - 1; i++)
            {
                var line = lines[i].Split(",");
                var nextLine = lines[i + 1].Split(",");

                ret.Add(CreateSublord(line, Convert.ToDouble(line[9]), Convert.ToDouble(nextLine[9])));
            }

            return ret;
        }

        private static SubLord CreateSublord(string[] line, double currentDegree, double nextDegree)
        {
            return new SubLord(Convert.ToInt32(line[0]), line[4], line[5], line[6], line[7], line[8],
                                 new DegreeRange(currentDegree, nextDegree));
        }

    }
}
