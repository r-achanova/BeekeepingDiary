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
                //UserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value
                UserId = this.User.GetId()
            };

            this.data.BeeGardens.Add(beeGardenData);
            this.data.SaveChanges();

            return RedirectToAction("All", "BeeGardens");
        }
    }
}
