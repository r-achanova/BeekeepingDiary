using Microsoft.AspNetCore.Mvc;
using BeekeepingDiary.Services.BeeGardens;
using BeekeepingDiary.Services.Statistics;

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

        public IActionResult Error() => View();
    }
}
