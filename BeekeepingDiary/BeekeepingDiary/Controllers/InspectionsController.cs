using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BeekeepingDiary.Services.Beehives;
using BeekeepingDiary.Services.Inspections;
using BeekeepingDiary.Infrastructure;
using BeekeepingDiary.Models.Inspections;

namespace BeekeepingDiary.Controllers
{
    public class InspectionsController : Controller
    {
        private readonly IInspectionService inspections;
        private readonly IBeehiveService beehives;
        
        public InspectionsController(IInspectionService inspections, IBeehiveService beehives)
        {
            this.beehives = beehives;
            this.inspections = inspections;
        }
        [Authorize]
        public IActionResult Add([FromQuery] InspectionQueryModel query)
        {
            return View(new InspectionFormModel
            {
                BeehiveName = this.beehives.GetBeehiveName(query.BeehiveId),
                Date = DateTime.Now
            });
        }
      
        [HttpPost]
        [Authorize]
        public IActionResult Add(InspectionFormModel inspection)
        {
            if (!ModelState.IsValid)
            {
                inspection.BeehiveName = this.beehives.GetBeehiveName(inspection.BeehiveId);


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
                BeehiveName = this.beehives.GetBeehiveName(inspection.BeehiveId),
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
                inspection.BeehiveName = this.beehives.GetBeehiveName(inspection.BeehiveId);

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
            query.BeehiveName = this.beehives.GetBeehiveName(query.BeehiveId);

            return View(query);
        }
    }
}
