namespace AstroTools.Common.Model.Degree
{
    public record DegreeRange(Degree Start, Degree End)
    {
        public bool Contains(Degree degree) => Start <= degree && degree < End;

        public DegreeRange(int start, int end) : this(new Degree(start), new Degree(end))
        {
            
        }
        
        public DegreeRange(uint start, uint end) : this(new Degree(start), new Degree(end))
        {
            
        }
    }

}
