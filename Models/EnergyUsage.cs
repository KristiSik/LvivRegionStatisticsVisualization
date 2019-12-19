using System.Collections.Generic;

namespace LvivRegionStatisticsVisualization.Models
{
    public class EnergyUsage
    {
        public List<int> Years { get; set; }

        public string TerritoryName { get; set; }

        public List<EnergyType> EnergyTypes { get; set; }
    }
}
