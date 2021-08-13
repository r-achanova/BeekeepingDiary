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
       
        private readonly IBeehiveService beehives;
        private readonly IBeeGardenService beeGardens;

        public BeehivesController(IBeeGardenService beeGardens, IBeehiveService beehives)
        {
            this.beeGardens = beeGardens;
            this.beehives = beehives;
        }
        [Authorize]
        public IActionResult All([FromQuery] AllBeehivesQueryModel query)
        {
            var queryResult = this.beehives.All(
                query.CurrentPage,
                AllBeehivesQueryModel.BeehivesPerPage, 
                this.User.GetId(),
                query.BeeGardenId);//1 do not use in this moment
            query.TotalBeehives = queryResult.TotalBeehives;
            query.Beehives = queryResult.Beehives;

            return View(query);
        }

        [Authorize]
        public IActionResult Mine([FromQuery] AllBeehivesQueryModel query)
        {
            var queryResult = this.beehives.Mine(
                query.CurrentPage,
                AllBeehivesQueryModel.BeehivesPerPage,
                this.User.GetId());
            query.TotalBeehives = queryResult.TotalBeehives;
            query.Beehives = queryResult.Beehives;

            return View(query);
        }
        [Authorize]
        public IActionResult Add()
        {
            return View(new BeehiveFormModel
            {
                Categories = this.beehives.AllCategories(),
                BeeGardens = this.beehives.AllBeeGardens(this.User.GetId()),
            });
        }

        [HttpPost]
        [Authorize]
        
        public IActionResult Add(BeehiveFormModel beehive)
        {
            if (!ModelState.IsValid)
            {
                beehive.Categories = this.beehives.AllCategories();
                beehive.BeeGardens = this.beehives.AllBeeGardens(this.User.GetId());
                return View(beehive);
            }

           this.beehives.Create(
                beehive.Name,
                beehive.ImageUrl,
                beehive.Year,
                beehive.CategoryId,
                beehive.BeeGardenId
                );
          
            return RedirectToAction(nameof(All), new { beeGardenId = beehive.BeeGardenId });
        }

       /* private IEnumerable<BeehiveCategoryViewModel> GetBeehiveCategories()
            => this.data
                .Categories
                .Select(c => new BeehiveCategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();*/

       /* private IEnumerable<BeehiveBeeGardenServiceModel> GetCurrentUserBeeGardens()
            => this.data
                .BeeGardens
                .Where(b => b.UserId == User.GetId())
                .Select(b => new BeehiveBeeGardenServiceModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    UserId=b.UserId
                })
                 .ToList();*/
    }
}
