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
        public IActionResult All([FromQuery] AllBeeGardensQueryModel query)
        {
            var beeGardensQuery = this.data.BeeGardens.AsQueryable();
            beeGardensQuery = beeGardensQuery.OrderByDescending(b => b.Year);
            var totalBeeGardens = beeGardensQuery.Count();

            var beeGardens = beeGardensQuery
            .Skip((query.CurrentPage - 1) * AllBeeGardensQueryModel.BeeGardensPerPage)
            .Take(AllBeeGardensQueryModel.BeeGardensPerPage)
            .Select(c => new BeeGardenListingViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Location = c.Location,
                Year = c.Year,
                ImageUrl = c.ImageUrl,
            })
            .ToList();

            query.TotalBeeGardens = totalBeeGardens;
            query.BeeGardens = beeGardens;

            return View(query);
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
