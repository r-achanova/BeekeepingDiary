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

        public int BeehiveId { get; set; }
        public IEnumerable<InspectionBeehiveServiceModel> Beehives { get; set; }
        public string BeehiveName { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}
