using System.ComponentModel;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

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
        
        public static string CreateHash(this string text)
        {
            using (SHA256 sha256Hash = SHA256.Create()) {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(text));

                StringBuilder builder = new StringBuilder();

                foreach (var value in bytes) {
                    builder.Append(value.ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}