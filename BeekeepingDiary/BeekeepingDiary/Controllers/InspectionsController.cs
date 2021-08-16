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
    public class InspectionsController:Controller
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
        public IActionResult Add(int id)
        {
            return View(new InspectionFormModel
            {
                BeehiveName = this.beehives.Details(id).BeeGarden + ": " + this.beehives.Details(id).Name
               
            });
        }

        [HttpPost]
        [Authorize]

        public IActionResult Add(InspectionFormModel inspection)
        {
            if (!ModelState.IsValid)
            {
                inspection.BeehiveName = this.beehives.Details(inspection.Id).BeeGarden + ": " + this.beehives.Details(inspection.Id).Name;


                return View(inspection);
            }

            this.inspections.Create(
                 inspection.Date,
                 inspection.Id,
                 inspection.Description
                 );

            return RedirectToAction(nameof(All), new { beehiveId = inspection.Id });
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
                BeehiveName = this.beehives.Details(inspection.Id).BeeGarden + ": " + this.beehives.Details(inspection.Id).Name,
            Description= inspection.Description,
                
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, InspectionFormModel inspection)
        {

            var userId = this.User.GetId();
            if (!ModelState.IsValid)
            {
                inspection.BeehiveName = this.beehives.Details(inspection.Id).BeeGarden + ": " + this.beehives.Details(inspection.Id).Name;


                return View(inspection);
            }

            this.inspections.Edit(
                id,
                inspection.Id,
                 inspection.Date,
                 inspection.Description
                 );
            return RedirectToAction(nameof(All), new { beehiveId = inspection.Id });

         }

        [Authorize]
        public IActionResult All([FromQuery] AllInspectionsQueryModel query)
        {
            var queryResult = this.inspections.All(this.User.GetId(),
                                query.BeehiveId);
            query.Inspections= queryResult.Inspections;
           query.BeehiveName = this.beehives.Details(query.BeehiveId).BeeGarden + ": "
           + this.beehives.Details(query.BeehiveId).Name;
          
            return View(query);
        }

        
        [Authorize]
        public IActionResult Inspections([FromQuery] AllInspectionsQueryModel query, int beehiveId)
        {
            var queryResult = this.inspections.All(this.User.GetId(),
                                beehiveId);
            query.Inspections = queryResult.Inspections;

            return View(query);
        }

    }
}
