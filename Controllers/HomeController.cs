using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LvivRegionStatisticsVisualization.Models;
using LvivRegionStatisticsVisualization.Services;

namespace LvivRegionStatisticsVisualization.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStatisticsDataService _statisticsDataService;

        public HomeController(ILogger<HomeController> logger, IStatisticsDataService statisticsDataService)
        {
            _logger = logger;
            _statisticsDataService = statisticsDataService;
        }

        public IActionResult Index()
        {
            EnergyUsage energyUsage = _statisticsDataService.GetActualStatisticsData().Result;
            ViewData["EnergyUsage"] = energyUsage;
            return View(energyUsage);
        }

        public IActionResult StatisticsByYear()
        {
            EnergyUsage energyUsage = _statisticsDataService.GetActualStatisticsData().Result;
            ViewData["EnergyUsage"] = energyUsage;
            return View(energyUsage);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
