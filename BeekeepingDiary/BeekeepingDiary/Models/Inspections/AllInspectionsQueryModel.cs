using BeekeepingDiary.Services.Inspections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeekeepingDiary.Models.Inspections
{
    public class AllInspectionsQueryModel
    {
        public int BeehiveId { get; set; }
        public string BeehiveName { get; set; }
        public IEnumerable<InspectionServiceModel> Inspections { get; set; }
    }
}
