namespace EphemerisMapper.Model.Units
{
    public record DegreeRange(Degree Start, Degree End)
    {
        public bool Contains(Degree degree) => Start <= degree && degree < End;
    }

}
