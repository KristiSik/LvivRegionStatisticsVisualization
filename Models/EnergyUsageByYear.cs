namespace LvivRegionStatisticsVisualization.Models
{
    public class EnergyUsageByYear
    {
        public int Year { get; set; }

        public double? UsedEnergy { get; set; }

        public EnergyUsageByYear(int year, double? usedEnergy)
        {
            Year = year;
            UsedEnergy = usedEnergy;
        }
    }
}
