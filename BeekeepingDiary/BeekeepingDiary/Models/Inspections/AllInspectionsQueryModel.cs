using System.Collections.Generic;
using BeekeepingDiary.Services.Inspections;


namespace BeekeepingDiary.Models.Inspections
{
    public class AllInspectionsQueryModel
    {
        public int BeehiveId { get; set; }
        public string BeehiveName { get; set; }
        public IEnumerable<InspectionServiceModel> Inspections { get; set; }
    }
}
