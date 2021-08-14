using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeekeepingDiary.Services.Inspections
{
    public class InspectionServiceModel
    {
        public int Id { get; init; }

        public DateTime Date { get; set; }

        public string Beehive { get; set; }

        public string Description { get; set; }
    }
}
