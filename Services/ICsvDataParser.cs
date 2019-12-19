using LvivRegionStatisticsVisualization.Models;

namespace LvivRegionStatisticsVisualization.Services
{
    public interface ICsvDataParser
    {
        EnergyUsage ParseCsvData(string inputString);
    }
}
