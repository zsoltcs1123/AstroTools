using System.Reflection;

namespace AstroTools.Common.Extensions;

public static class EnumExtensions
{
    public static T Get<T>(this Enum enumValue) where T : Attribute
    {
        return enumValue.GetType().GetMember(enumValue.ToString()).First().GetCustomAttribute<T>();
    }
}