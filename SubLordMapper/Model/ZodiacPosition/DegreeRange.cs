namespace SubLordMapper.Model.ZodiacPosition
{
    public record DegreeRange(Degree Start, Degree End)
    {
        public bool Contains(Degree degree) => Start <= degree && degree < End;
    }

}
