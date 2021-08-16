using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeekeepingDiary.Services.Inspections
{
    public class InspectionQueryServiceModel
    {
        public int BeehiveId { get; set; }
        public IEnumerable<InspectionServiceModel> Inspections { get; init; }
    }
}
