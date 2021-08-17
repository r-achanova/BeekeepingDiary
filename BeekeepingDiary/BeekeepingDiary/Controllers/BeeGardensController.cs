using BeekeepingDiary.Data;
using BeekeepingDiary.Data.Models;
using BeekeepingDiary.Models.BeeGardens;
using BeekeepingDiary.Services.BeeGardens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BeekeepingDiary.Infrastructure;


namespace BeekeepingDiary.Controllers
{
    public class BeeGardensController : Controller
    {
        private readonly IBeeGardenService beeGardens;
      
        public BeeGardensController(IBeeGardenService beeGardens)
        {
            this.beeGardens = beeGardens;
        }
        [Authorize]
        public IActionResult All([FromQuery] AllBeeGardensQueryModel query)
        {
            var queryResult = this.beeGardens.AllForUser(
                query.CurrentPage,
                AllBeeGardensQueryModel.BeeGardensPerPage,
                this.User.GetId());

            query.TotalBeeGardens = queryResult.TotalBeeGardens;
            query.BeeGardens = queryResult.BeeGardens;

            return View(query);
        }

        [Authorize]
        public IActionResult Details(int id)
        {
           return RedirectToAction("All", "Beehives", new { beeGardenId = id });
        }

        /* [Authorize]
         public IActionResult Mine()
         {
             var myBeeGardens = this.beeGardens.ByUser(this.User.GetId());

             return View(myBeeGardens);
         }
        */
        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(BeeGardenFormModel beeGarden)
        {
            if (!ModelState.IsValid)
            {
                return View(beeGarden);
            }

            var id=this.beeGardens.Create(
                beeGarden.Name,
                beeGarden.Location,
                beeGarden.ImageUrl,
                beeGarden.Year,
                this.User.GetId());

           return RedirectToAction("All", "Beehives",new {beeGardenId=id});
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.GetId();

            var beeGarden = this.beeGardens.Details(id);

            if (beeGarden.UserId != userId)
            {
                return Unauthorized();
            }

            return View(new BeeGardenFormModel
            {
                Name = beeGarden.Name,
                Location = beeGarden.Location,
                ImageUrl = beeGarden.ImageUrl,
                Year = beeGarden.Year,
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, BeeGardenFormModel beeGarden)
        {
            var userId = this.User.GetId();

            if (!ModelState.IsValid)
            {
                return View(beeGarden);
            }

            if (!this.beeGardens.IsByCurrentUser(id, userId))
            {
                return BadRequest();
            }

            this.beeGardens.Edit(
                id,
                beeGarden.Name,
                beeGarden.Location,
                beeGarden.ImageUrl,
                beeGarden.Year
                );

            return RedirectToAction(nameof(All));
        }

    }
}
