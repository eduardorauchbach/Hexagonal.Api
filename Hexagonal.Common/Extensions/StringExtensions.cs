using System.Text;
using System.Text.RegularExpressions;

namespace Hexagonal.Common.Extensions
{
    public static class StringExtensions
    {
        public static string ToFlat(this string value) => Regex.Replace(value.Trim(), @"[\ ]+", " ").Replace("\r\n", "").Replace("\n", "");
        public static string ToFlat(this StringBuilder value) => value.ToString().ToFlat();

        public static Stream FromBase64ToStream(this string value)
        {
            byte[] fileBytes = Convert.FromBase64String(value);
            return fileBytes.ToStream();
        }

        public static string GetExtension(this string filename)
        {
            return filename.Split('.').Last();
        }
    }
}
