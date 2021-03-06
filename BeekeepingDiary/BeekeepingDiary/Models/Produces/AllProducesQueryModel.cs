using BeekeepingDiary.Services.Produces;
using System.Collections.Generic;

namespace BeekeepingDiary.Models.Produces
{
    public class AllProducesQueryModel
    {
        public int BeehiveId { get; set; }
        public string BeehiveName { get; set; }
        public double TotalProduces { get; set; }
        public double AverageProduces { get; set; }
        public IEnumerable<ProduceServiceModel> Produces { get; set; }
    }
}
