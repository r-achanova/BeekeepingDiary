using BeekeepingDiary.Data.Models;
using BeekeepingDiary.Services.Inspections;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace BeekeepingDiary.Models.Inspections
{
    public class InspectionFormModel
    {
        public int Id { get; init; }

        public DateTime Date { get; set; }

        // public int BeehiveId { get; set; }
        public string BeehiveName { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
