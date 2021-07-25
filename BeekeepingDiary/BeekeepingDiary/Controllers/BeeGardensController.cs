using BeekeepingDiary.Data;
using BeekeepingDiary.Data.Models;
using BeekeepingDiary.Models.BeeGardens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BeekeepingDiary.Data.DataConstants.BeeGarden;

namespace BeekeepingDiary.Controllers
{
    public class BeeGardensController : Controller
    {
        private readonly BeekeepingDbContext data;

        public BeeGardensController(BeekeepingDbContext data)
        {
            this.data = data;
        }

        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(AddBeeGardenFormModel beeGarden)
        {
            if (!ModelState.IsValid)
            {
                return View(beeGarden);
            }
            var beeGardenData = new BeeGarden
            {
                Name = beeGarden.Name,
                Location = beeGarden.Location,
                ImageUrl = beeGarden.ImageUrl,
                Year = beeGarden.Year,
                
            };

            this.data.BeeGardens.Add(beeGardenData);
            this.data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}
