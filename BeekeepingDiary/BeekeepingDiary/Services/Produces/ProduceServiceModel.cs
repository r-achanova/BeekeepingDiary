using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeekeepingDiary.Services.Produces
{
    public class ProduceServiceModel
    {
        public int Id { get; init; }
        public DateTime Date { get; set; }
        public int BeehiveId { get; set; }
        public string Beehive { get; set; }
        public double HoneyKg { get; set; }
        public string HoneyType { get; set; }
        public string Notes { get; set; }
    }
}
