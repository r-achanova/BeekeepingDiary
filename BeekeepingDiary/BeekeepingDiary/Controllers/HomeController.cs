using BeekeepingDiary.Data;
using BeekeepingDiary.Models;
using BeekeepingDiary.Models.Home;
using BeekeepingDiary.Services.BeeGardens;
using BeekeepingDiary.Services.Statistics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;


namespace BeekeepingDiary.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;
        private readonly IBeeGardenService beeGardens;
        
        public HomeController(
            IStatisticsService statistics,
            IBeeGardenService beeGardens)
        {
            this.statistics = statistics;
            this.beeGardens = beeGardens;
        }
        public IActionResult Index()
        {
            var totalStatistics = this.statistics.Total();
            var beeGardens = this.beeGardens.Index();

           

            return View(new IndexServiceModel
            {
                TotalBeeGardens = totalStatistics.TotalBeeGardens,
                TotalUsers = totalStatistics.TotalUsers,
                BeeGardens = (System.Collections.Generic.List<BeeGardenServiceModel>)beeGardens
            });
        }

       // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    

        
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
