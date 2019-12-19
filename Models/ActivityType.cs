using System.Collections.Generic;

namespace LvivRegionStatisticsVisualization.Models
{
    public class ActivityType
    {
        public string Name { get; set; }

        public List<EnergyUsageByYear> EnergyUsage { get; set; }

        public List<ActivityType> Subtypes { get; set; }
    }
}
