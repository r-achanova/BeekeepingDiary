using BeekeepingDiary.Infrastructure;
using BeekeepingDiary.Models.Produces;
using BeekeepingDiary.Services.Beehives;
using BeekeepingDiary.Services.Produces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeekeepingDiary.Controllers
{
    public class ProducesController : Controller
    {
        private readonly IBeehiveService beehives;
        private readonly IProduceService produces;

        public ProducesController(IBeehiveService beehives, IProduceService produces)
        {
            this.produces = produces;
            this.beehives = beehives;
        }
        [Authorize]
        public IActionResult Add([FromQuery] ProduceQueryModel query)
        {
            return View(new ProduceFormModel
            {
                BeehiveName = this.beehives.GetBeehiveName(query.BeehiveId)

            });
        }

        [HttpPost]
        [Authorize]

        public IActionResult Add(ProduceFormModel produce)
        {
            if (!ModelState.IsValid)
            {
                produce.BeehiveName = this.beehives.GetBeehiveName(produce.BeehiveId);


                return View(produce);
            }

            this.produces.Create(
                 produce.Date,
                 produce.BeehiveId,
                 produce.HoneyKg,
                 produce.HoneyType,
                 produce.Notes
                 );

            return RedirectToAction(nameof(All), new { beehiveId = produce.BeehiveId });
        }

        [Authorize]
        public IActionResult All([FromQuery] AllProducesQueryModel query)
        {
            var queryResult = this.produces.All(this.User.GetId(),
                                query.BeehiveId);
            query.Produces = queryResult.Produces;
            query.AverageProduces = queryResult.AverageProduce;
            query.TotalProduces = queryResult.TotalProduce;
            query.BeehiveName = this.beehives.GetBeehiveName(query.BeehiveId);

            return View(query);
        }
    }
}
