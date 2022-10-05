using SubLordMapper.Model.Divisions;
using SubLordMapper.Model.Enums;
using SubLordMapper.Model.ZodiacPosition;

namespace SubLordMapper.Model.Mappers;

public static class SubLordMapper
{
    private readonly static List<Nakshatra> Nakshatras = Enum.GetValues<Star>()
        .Select(s => new Nakshatra(s)).ToList();

    private readonly static List<SubLordNew> SubLords = GenerateSubLords().ToList();

    private static IEnumerable<SubLordNew> GenerateSubLords()
    {
        int id = 0;
        var offset = new Degree(-13, 0, 0);
        return Nakshatras.SelectMany(nakshatra =>
        {
            offset += new Degree(13, 0, 0);
            return nakshatra.SubDivisions[0].Ranges.Select(drp =>
            {
                var sign = drp.Key.Start.ToSign();
                var signLord = sign.ToLord();
                var star = nakshatra.Star;
                var starLord = star.ToLord();
                var sub = drp.Value;
                var degrees = new DegreeRange(drp.Key.Start + offset, drp.Key.End + offset);

                return new SubLordNew(id++, sign, signLord, star, starLord, sub, degrees);
            });
        });
    }

    public static SubLordNew ToSubLord(this Degree degree)
    {
        return SubLords.First(sl => sl.Degrees.Contains(degree));
    }
}