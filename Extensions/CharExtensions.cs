namespace LvivRegionStatisticsVisualization.Extensions
{
    public static class CharExtensions
    {
        public static bool IsCapital(this char c)
        {
            return c <= 'Я';
        }
    }
}
