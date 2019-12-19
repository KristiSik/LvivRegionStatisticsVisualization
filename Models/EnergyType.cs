using System.Collections.Generic;

namespace LvivRegionStatisticsVisualization.Models
{
    public class EnergyType
    {
        public string Name { get; set; }

        public List<ActivityType> Activities { get; set; }
    }
}
