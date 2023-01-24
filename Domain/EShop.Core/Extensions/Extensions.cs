using System.ComponentModel;
using System.Reflection;

namespace EShop.Core.Extensions
{
    public static class Extensions
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.FirstOrDefault()?.Description;
            }

            return value.ToString();
        }

        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> enumerable)
        {
            var result = new HashSet<T>();
            foreach (var item in enumerable)
            {
                result.Add(item);
            }
            return result;
        }
    }
}