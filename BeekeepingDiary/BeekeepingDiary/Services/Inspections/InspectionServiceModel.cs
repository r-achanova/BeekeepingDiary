using System;

namespace BeekeepingDiary.Services.Inspections
{
    public class InspectionServiceModel
    {
        public int Id { get; init; }
        public DateTime Date { get; set; }
        public int BeehiveId { get; set; }
        public string Beehive { get; set; }
        public string Description { get; set; }
    }
}
