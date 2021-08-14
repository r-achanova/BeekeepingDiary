using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeekeepingDiary.Services.Inspections
{
    public class InspectionBeehiveServiceModel
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string UserId { get; set; }
    }
}
