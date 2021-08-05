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
        private readonly BeekeepingDbContext data;

        public BeeGardensController(IBeeGardenService beeGardens, BeekeepingDbContext data)
        {
            this.beeGardens = beeGardens;
            this.data = data;
        }
        [Authorize]
        public IActionResult All([FromQuery] AllBeeGardensQueryModel query)
        {
            var queryResult = this.beeGardens.All(
                query.CurrentPage,
                AllBeeGardensQueryModel.BeeGardensPerPage,
                this.User.GetId());

            query.TotalBeeGardens = queryResult.TotalBeeGardens;
            query.BeeGardens = queryResult.BeeGardens;

            return View(query);
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

            this.beeGardens.Create(
                beeGarden.Name,
                beeGarden.Location,
                beeGarden.ImageUrl,
                beeGarden.Year,
                this.User.GetId());

            return RedirectToAction("All", "BeeGardens");
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

            return RedirectToAction("All", "BeeGardens");
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, BeeGardenFormModel beeGarden)
        {
            var userId = this.User.GetId();

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
