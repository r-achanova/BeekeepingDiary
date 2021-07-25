using BeekeepingDiary.Data;
using BeekeepingDiary.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;


namespace BeekeepingDiary.Controllers
{
    public class HomeController : Controller
    {
        private readonly BeekeepingDbContext data;
        public HomeController(BeekeepingDbContext data)
        {
            this.data = data;
        }
        public IActionResult Index()
        {
            return View();
        }

        
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
