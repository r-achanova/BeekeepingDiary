using BeekeepingDiary.Data.Models;
using System;
using System.ComponentModel.DataAnnotations;


namespace BeekeepingDiary.Models.Inspections
{
    public class InspectionFormModel
    {
        public int Id { get; init; }

        public DateTime Date { get; set; }

        public int BeehiveId { get; set; }
        public Beehive Beehive { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
