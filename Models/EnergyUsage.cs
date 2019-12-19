using System.Collections.Generic;

namespace LvivRegionStatisticsVisualization.Models
{
    public class EnergyUsage
    {
        public string TerritoryName { get; set; }

        public List<EnergyType> EnergyTypes { get; set; }

        public List<int> Years { get; set; }
    }
}
