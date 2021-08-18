using System.Collections.Generic;

namespace BeekeepingDiary.Services.Produces
{
    public class ProduceQueryServiceModel
    {
        public int BeehiveId { get; set; }
        public IEnumerable<ProduceServiceModel> Produces { get; init; }
        public double TotalProduce { get; set; }
        public double AverageProduce { get; set; }
    }
}
