using System.Threading.Tasks;

namespace LvivRegionStatisticsVisualization.Services
{
    public interface IStatisticsDataService
    {
        Task GetActualStatisticsData();
    }
}
