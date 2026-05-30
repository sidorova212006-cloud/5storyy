using System.Globalization;

namespace plug.Services
{
    public static class NumberHelper
    {
        public static double ParseDouble(string value)
        {
            return double.TryParse(
                    value,
                    NumberStyles.Any,
                    CultureInfo.InvariantCulture,
                    out var result)
                ? result
                : 0;
        }

        public static string FormatResult(double value, int precision)
        {
            return value.ToString($"F{precision}");
        }
    }
}
