using System;
using System.Collections.Generic;

namespace LvivRegionStatisticsVisualization.Models
{
    public class EnergyTypeWithActivities : EnergyType
    {
        public List<ActivityType> Activities { get; set; }
    }
}
