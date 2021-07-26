using BeekeepingDiary.Data;
using BeekeepingDiary.Models;
using BeekeepingDiary.Models.Home;
using BeekeepingDiary.Services.Statistics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;


namespace BeekeepingDiary.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;
        private readonly BeekeepingDbContext data;
        public HomeController(
            IStatisticsService statistics, 
            BeekeepingDbContext data)
        {
            this.statistics = statistics;
            this.data = data;
        }
        public IActionResult Index()
        {
            var totalStatistics = this.statistics.Total();

            var beeGardens = this.data
                .BeeGardens
                .OrderBy(b => b.Id)
                .Select(b => new BeeGardenIndexViewModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    Location = b.Location,
                    Year = b.Year,
                    ImageUrl = b.ImageUrl
                })
                .ToList();

            return View(new IndexViewModel
            {
                TotalBeeGardens = totalStatistics.TotalBeeGardens,
                TotalUsers = totalStatistics.TotalUsers,
                BeeGardens = beeGardens
            });
        }

       // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    

        
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
