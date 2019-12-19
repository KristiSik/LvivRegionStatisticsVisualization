using System.Collections.Generic;

namespace LvivRegionStatisticsVisualization.Models
{
    public class City
    {
        public string Name { get; set; }

        public List<EnergyUsageByYear> EnergyUsage { get; set; }
    }
}
