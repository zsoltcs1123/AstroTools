using System.Reflection;

namespace EphemerisMapper.Extensions;

public static class EnumExtensions
{
    public static T Get<T>(this Enum enumValue) where T : Attribute
    {
        return enumValue.GetType().GetMember(enumValue.ToString()).First().GetCustomAttribute<T>();
    }
}