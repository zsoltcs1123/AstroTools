using EphemerisMapper.Model.ZodiacPosition;

namespace EphemerisMapper.Model.Mappers;

public interface IDegreeMapper<out T>
{
    public T Map(Degree degree);
}