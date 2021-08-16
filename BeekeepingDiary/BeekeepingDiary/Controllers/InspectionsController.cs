using BeekeepingDiary.Services.BeeGardens;
using BeekeepingDiary.Services.Beehives;
using BeekeepingDiary.Services.Inspections;
using BeekeepingDiary.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using BeekeepingDiary.Models.Inspections;

namespace BeekeepingDiary.Controllers
{
    public class InspectionsController : Controller
    {
        private readonly IInspectionService inspections;
        private readonly IBeehiveService beehives;
        private readonly IBeeGardenService beeGardens;

        public InspectionsController(IInspectionService inspections, IBeehiveService beehives, IBeeGardenService beeGardens)
        {
            this.beeGardens = beeGardens;
            this.beehives = beehives;
            this.inspections = inspections;
        }

        [Authorize]
        public IActionResult Add([FromQuery] AllInspectionsQueryModel query) //to do update query model
        {
            return View(new InspectionFormModel
            {
                BeehiveName = this.beehives.Details(query.BeehiveId).BeeGarden + ": " + this.beehives.Details(query.BeehiveId).Name

            });
        }
      
        [HttpPost]
        [Authorize]

        public IActionResult Add(InspectionFormModel inspection)
        {
            if (!ModelState.IsValid)
            {
                inspection.BeehiveName = this.beehives.Details(inspection.BeehiveId).BeeGarden + ": " + this.beehives.Details(inspection.BeehiveId).Name;


                return View(inspection);
            }
  
            this.inspections.Create(
                 inspection.Date,
                 inspection.BeehiveId, 
                 inspection.Description
                 );

            return RedirectToAction(nameof(All), new { beehiveId = inspection.BeehiveId });
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.GetId();

            var inspection = this.inspections.Details(id);
            if (inspection.UserId != userId)
            {
                return Unauthorized();
            }

            return View(new InspectionFormModel
            {
                Date = inspection.Date,
                BeehiveName = this.beehives.Details(inspection.BeehiveId).BeeGarden + ": " + this.beehives.Details(inspection.BeehiveId).Name,
                Description = inspection.Description,
                BeehiveId = inspection.BeehiveId
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, InspectionFormModel inspection)
        {

            var userId = this.User.GetId();
            if (!ModelState.IsValid)
            {
                inspection.BeehiveName = this.beehives.Details(inspection.BeehiveId).BeeGarden + ": " + this.beehives.Details(inspection.BeehiveId).Name;

                return View(inspection);
            }
            inspection.BeehiveId = inspections.Details(inspection.Id).BeehiveId;
            this.inspections.Edit(
                id,
                inspection.Date,
                inspection.Description
                 );
            return RedirectToAction(nameof(All), new { beehiveId = inspection.BeehiveId });

        }

        [Authorize]
        public IActionResult All([FromQuery] AllInspectionsQueryModel query)
        {
            var queryResult = this.inspections.All(this.User.GetId(),
                                query.BeehiveId);
            query.Inspections = queryResult.Inspections;
            query.BeehiveName = this.beehives.Details(query.BeehiveId).BeeGarden + ": "
            + this.beehives.Details(query.BeehiveId).Name;

            return View(query);
        }


       /* [Authorize]
        public IActionResult Inspections([FromQuery] AllInspectionsQueryModel query, int beehiveId)
        {
            var queryResult = this.inspections.All(this.User.GetId(),
                                beehiveId);
            query.Inspections = queryResult.Inspections;

            return View(query);
        }*/

    }
}
