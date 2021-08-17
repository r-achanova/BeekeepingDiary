using BeekeepingDiary.Models.Produces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeekeepingDiary.Services.Produces
{
  public interface IProduceService
    {
        public ProduceQueryServiceModel All(
            string userId,
            int beehiveId);
        int Create(
            DateTime date,
            int beehiveId,
            double honeyKg, 
            string honeyType, 
            string notes);

        public ProduceDetailsServiceModel Details(int id);
        public bool Edit(int produceId, DateTime date, double honeyKg, string honeyType, string notes);
        public IEnumerable<ProduceServiceModel> GetProducesByBeehiveId(int beehiveId);
        public double GetAverageProduces(int beehiveId);
        public double GetTotalProduces(int beehiveId);
    }
}
