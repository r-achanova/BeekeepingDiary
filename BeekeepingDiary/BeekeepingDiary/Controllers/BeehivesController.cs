using BeekeepingDiary.Infrastructure;
using BeekeepingDiary.Models.Beehives;
using BeekeepingDiary.Services.Beehives;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeekeepingDiary.Controllers
{
    public class BeehivesController:Controller
    {
        private readonly IBeehiveService beehives;
        
        public BeehivesController(IBeehiveService beehives)
        {
           this.beehives = beehives;
        }

        public void Details()
        {
            throw new System.NotImplementedException();
        }

        [Authorize]
        public IActionResult All([FromQuery] AllBeehivesQueryModel query)
        {
            var queryResult = this.beehives.All(
                query.CurrentPage,
                AllBeehivesQueryModel.BeehivesPerPage, 
                this.User.GetId(),
                query.BeeGardenId);
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
          
            return RedirectToAction(nameof(All), new { currentPage=1, beehivesPerPage=3, userId= this.User.GetId(), beeGardenId = beehive.BeeGardenId });
        }
        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.GetId();

            var beehive = this.beehives.Details(id);
            if (beehive.UserId != userId )
            {
                return Unauthorized();
            }


            return View(new BeehiveFormModel
            {
                Name = beehive.Name,
                ImageUrl = beehive.ImageUrl,
                Year = beehive.Year,
                CategoryId = beehive.CategoryId,
                Categories=beehives.AllCategories(),
                BeeGardenId = beehive.BeeGardenId,
                BeeGardens=beehives.AllBeeGardens(userId)
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, BeehiveFormModel beehive)
        {
            var userId = this.User.GetId();

            if (!ModelState.IsValid)
            {
                beehive.Categories = this.beehives.AllCategories();
                beehive.BeeGardens = this.beehives.AllBeeGardens(this.User.GetId());
                return View(beehive);
            }
           
            this.beehives.Edit(
                id,
                beehive.Name,
                beehive.ImageUrl,
                beehive.Year,
                beehive.CategoryId,
                beehive.BeeGardenId
                );

            return RedirectToAction(nameof(All), new { currentPage = 1, beehivesPerPage = 3, userId = this.User.GetId(), beeGardenId = beehive.BeeGardenId });
        }

        [Authorize]
        public IActionResult Inspections(int id)
        {
            return RedirectToAction("All", "Inspections", new { beehiveId = id });
        }

        [Authorize]
        public IActionResult Produces(int id)
        {
            return RedirectToAction("All", "Produces", new { beehiveId = id });
        }
    }
}

