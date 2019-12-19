using System.Threading.Tasks;
using LvivRegionStatisticsVisualization.Enums;
using LvivRegionStatisticsVisualization.Models;

namespace LvivRegionStatisticsVisualization.Services
{
    public interface IStatisticsDataService
    {
        Task<EnergyUsage> GetActualStatisticsByActivityTypeData();

        Task<EnergyUsage> GetActualStatisticsByCityData();
    }
}
