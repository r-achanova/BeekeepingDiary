using System.Collections.Generic;

namespace BeekeepingDiary.Services.Inspections
{
    public class InspectionQueryServiceModel
    {
        public int BeehiveId { get; set; }
        public IEnumerable<InspectionServiceModel> Inspections { get; init; }
    }
}
