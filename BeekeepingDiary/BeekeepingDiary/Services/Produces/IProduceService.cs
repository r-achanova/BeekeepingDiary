using System;
using System.Collections.Generic;

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
        public bool Delete(int produceId);
    }
}
