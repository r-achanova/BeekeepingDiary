using BeekeepingDiary.Data;
using BeekeepingDiary.Data.Models;
using BeekeepingDiary.Models.Produces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeekeepingDiary.Services.Produces
{
    public class ProduceService : IProduceService
    {
        private readonly BeekeepingDbContext data;
        public ProduceService(BeekeepingDbContext data)
        {
            this.data = data;
        }

        public ProduceQueryServiceModel All(string userId, int beehiveId)
        {
            var produces = GetProducesByBeehiveId(beehiveId);
            produces = produces.OrderByDescending(b => b.Date);
            var totalProduces = Math.Round(GetTotalProduces(beehiveId), 2);
            var averageProduces = Math.Round(GetAverageProduces(beehiveId), 2);

            return new ProduceQueryServiceModel
            {
                TotalProduce=totalProduces,
                AverageProduce=averageProduces,
                Produces = produces
            };
        }

        public int Create(DateTime date, int beehiveId, double honeyKg, string honeyType, string notes)
        {
            var produceData = new Produce
            {
                Date = date,
                BeehiveId = beehiveId,
                HoneyKg=honeyKg,
                HoneyType=honeyType,
               Notes=notes
            };
            this.data.Produces.Add(produceData);
            this.data.SaveChanges();

            return produceData.Id;
        }

        public ProduceDetailsServiceModel Details(int id)
            => this.data
                .Produces
                .Where(p => p.Id == id)
                .Select(p => new ProduceDetailsServiceModel
                {
                    Id = p.Id,
                    Date = p.Date,
                    HoneyKg = p.HoneyKg,
                    HoneyType = p.HoneyType,
                    Notes = p.Notes,
                    BeehiveId = p.BeehiveId,
                    Beehive = p.Beehive.Name,
                    UserId = p.Beehive.BeeGarden.UserId
                })
                .FirstOrDefault();

        public bool Edit(int produceId, DateTime date, double honeyKg, string honeyType, string notes)
        {
            var produceData = this.data.Produces.Find(produceId);
            if (produceData == null)
            {
                return false;
            }

            produceData.Date = date;
            produceData.HoneyKg = honeyKg;
            produceData.HoneyType = honeyType;
            produceData.Notes = notes;


            this.data.SaveChanges();

            return true;
        }

        public IEnumerable<ProduceServiceModel> GetProducesByBeehiveId(int beehiveId)
        {
            var query = this.data.Produces
               .Where(x => x.BeehiveId == beehiveId)
               .Select(x => new ProduceServiceModel
               {
                   Id = x.Id,
                   Date = x.Date,
                   Beehive = x.Beehive.Name,
                   HoneyKg = x.HoneyKg,
                   HoneyType=x.HoneyType,
                   Notes=x.Notes
               })
               .ToList();
            return query;
        }

        public double GetAverageProduces(int beehiveId)
        {
            var produces = this.data.Produces
                .Where(x => x.BeehiveId == beehiveId);
            if(produces.Count() > 0)
            {
                return produces.Average(x => x.HoneyKg);
            }
            return 0;
        }

        public double GetTotalProduces(int beehiveId)
        {

            var produces = this.data.Produces
                .Where(x => x.BeehiveId == beehiveId);
            if (produces.Count() > 0)
            {
                return produces.Sum(x => x.HoneyKg);
            }
            return 0;
        }

        
    }
}
