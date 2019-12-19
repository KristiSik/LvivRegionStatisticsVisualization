using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LvivRegionStatisticsVisualization.Models;
using LvivRegionStatisticsVisualization.Services;

namespace LvivRegionStatisticsVisualization.Controllers
{
    public class HomeController : Controller
    {
        private const int FetchDataFrequencyMinutes = 10;
        private readonly IStatisticsDataService _statisticsDataService;
        private DateTime _responseTime;
        private EnergyUsage _energyUsageByActivityType;
        private EnergyUsage _energyUsageByCity;

        public HomeController(IStatisticsDataService statisticsDataService)
        {
            _statisticsDataService = statisticsDataService;
            FetchData();
        }

        public IActionResult Index()
        {
            if ((DateTime.Now - _responseTime).TotalMinutes >= FetchDataFrequencyMinutes)
            {
                FetchData();
            }
            return View(_energyUsageByActivityType);
        }

        public IActionResult StatisticsByActivity()
        {
            if ((DateTime.Now - _responseTime).TotalMinutes >= FetchDataFrequencyMinutes)
            {
                FetchData();
            }
            return View(_energyUsageByActivityType);
        }

        public IActionResult StatisticsByProductActivity()
        {
            if ((DateTime.Now - _responseTime).TotalMinutes >= FetchDataFrequencyMinutes)
            {
                FetchData();
            }
            return View(_energyUsageByActivityType);
        }

        public IActionResult StatisticsByCity()
        {
            if ((DateTime.Now - _responseTime).TotalMinutes >= FetchDataFrequencyMinutes)
            {
                FetchData();
            }
            return View(_energyUsageByCity);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private void FetchData()
        {
            _energyUsageByActivityType = _statisticsDataService.GetActualStatisticsByActivityTypeData().Result;
            _energyUsageByCity = _statisticsDataService.GetActualStatisticsByCityData().Result;
            _responseTime = DateTime.Now;
        }
    }
}
