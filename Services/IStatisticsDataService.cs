using System.Threading.Tasks;
using LvivRegionStatisticsVisualization.Models;

namespace LvivRegionStatisticsVisualization.Services
{
    public interface IStatisticsDataService
    {
        Task<EnergyUsage> GetActualStatisticsData();
    }
}
