using System.Linq;

namespace LvivRegionStatisticsVisualization.Extensions
{
    public static class StringExtensions
    {
        public static bool IsValidRowValue(this string value)
        {
            return value.ToCharArray().Any(c => c != '\t' && c != ' ');
        }
    }
}
