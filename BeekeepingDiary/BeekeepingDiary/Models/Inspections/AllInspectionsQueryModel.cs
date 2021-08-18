using BeekeepingDiary.Services.Inspections;
using System.Collections.Generic;


namespace BeekeepingDiary.Models.Inspections
{
    public class AllInspectionsQueryModel
    {
        public int BeehiveId { get; set; }
        public string BeehiveName { get; set; }
        public IEnumerable<InspectionServiceModel> Inspections { get; set; }
    }
}
