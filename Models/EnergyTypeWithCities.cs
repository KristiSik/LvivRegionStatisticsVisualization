using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LvivRegionStatisticsVisualization.Models
{
    public class EnergyTypeWithCities : EnergyType
    {
        public List<City> Cities { get; set; }
    }
}
