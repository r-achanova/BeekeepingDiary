using System.Linq;
using BeekeepingDiary.Services.BeeGardens;
using Microsoft.AspNetCore.Mvc;

namespace BeekeepingDiary.Areas.Admin.Controllers
{
    public class BeeGardensController : AdminController
    {

        private readonly IBeeGardenService beeGardens;

        public BeeGardensController (IBeeGardenService beeGardens)
        {
            this.beeGardens = beeGardens;
        }
        public IActionResult All()
        {
            var beeGardens = this.beeGardens.All().BeeGardens.OrderBy(x=>x.UserId).ThenByDescending(x=>x.Year);

            return View(beeGardens);
        }
    }
}
