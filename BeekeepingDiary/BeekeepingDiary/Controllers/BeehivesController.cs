using BeekeepingDiary.Data;
using BeekeepingDiary.Data.Models;
using BeekeepingDiary.Models.Beehives;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeekeepingDiary.Controllers
{
    public class BeehivesController:Controller
    {
        private readonly BeekeepingDbContext data;

        public BeehivesController(BeekeepingDbContext data)
        {
            this.data = data;
        }

        [Authorize]
        public IActionResult Add()
        {
            return View(new AddBeehiveFormModel
            {
                Categories = this.GetBeehiveCategories(),
                BeeGardens = this.GetBeehiveBeeGardens(),
            });
        }

        [HttpPost]
        [Authorize]
        
        public IActionResult Add(AddBeehiveFormModel beehive)
        {
            if (!ModelState.IsValid)
            {
                return View(beehive);
            }
            var beehiveData = new Beehive
            {
                Name = beehive.Name,
                ImageUrl = beehive.ImageUrl,
                Year = beehive.Year,
                CategoryId = beehive.CategoryId,
                BeeGardenId = beehive.BeeGardenId,

            };

            this.data.Beehives.Add(beehiveData);
            this.data.SaveChanges();

            return RedirectToAction("Index", "Home");
            //return RedirectToAction(nameof(All));
        }

        private IEnumerable<BeehiveCategoryViewModel> GetBeehiveCategories()
            => this.data
                .Categories
                .Select(c => new BeehiveCategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();

        private IEnumerable<BeeGardenViewModel> GetBeehiveBeeGardens()
            => this.data
                .BeeGardens
                .Select(c => new BeeGardenViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();
    }
}
