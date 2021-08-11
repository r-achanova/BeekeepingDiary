using BeekeepingDiary.Data;
using BeekeepingDiary.Data.Models;
using BeekeepingDiary.Infrastructure;
using BeekeepingDiary.Models.Beehives;
using BeekeepingDiary.Services.BeeGardens;
using BeekeepingDiary.Services.Beehives;
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
        private readonly IBeehiveService beehives;
        private readonly IBeeGardenService beeGardens;

        public BeehivesController(IBeeGardenService beeGardens, IBeehiveService beehives, BeekeepingDbContext data)
        {
            this.beeGardens = beeGardens;
            this.beehives = beehives;
            this.data = data;
        }
        [Authorize]
        public IActionResult All([FromQuery] AllBeehivesQueryModel query)
        {
            var queryResult = this.beehives.All(
                query.CurrentPage,
                AllBeehivesQueryModel.BeehivesPerPage, 
                this.User.GetId(),
                1);//1 do not use in this moment
            query.TotalBeehives = queryResult.TotalBeehives;
            query.Beehives = queryResult.Beehives;

            return View(query);
        }


        [Authorize]
        public IActionResult Add()
        {
            return View(new BeehiveFormModel
            {
                Categories = this.GetBeehiveCategories(),
                BeeGardens = this.GetCurrentUserBeeGardens(),
            });
        }

        [HttpPost]
        [Authorize]
        
        public IActionResult Add(BeehiveFormModel beehive)
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

           // return RedirectToAction("Index", "Home");
            return RedirectToAction(nameof(All));
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

        private IEnumerable<BeeGardenViewModel> GetCurrentUserBeeGardens()
            => this.data
                .BeeGardens
                .Where(b => b.UserId == User.GetId())
                .Select(b => new BeeGardenViewModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    UserId=b.UserId
                })
                 .ToList();
    }
}
