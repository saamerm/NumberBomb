using System;
using System.Text.RegularExpressions;

namespace NumberBomb.Helper
{
    public static class StringHelpers
    {
        public static string SplitPascalCase(this string value)
        {
            return Regex.Replace(value, @"(?<=[A-Za-z])(?=[A-Z][a-z])|(?<=[a-z0–9])(?=[0–9]?[A-Z])", " ").Trim();
        }
    }
}