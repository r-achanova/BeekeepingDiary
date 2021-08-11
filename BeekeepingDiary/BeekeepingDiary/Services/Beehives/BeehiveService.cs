using BeekeepingDiary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeekeepingDiary.Services.Beehives
{
    public class BeehiveService : IBeehiveService
    {
        private readonly BeekeepingDbContext data;
        public BeehiveService(BeekeepingDbContext data)
        {
            this.data = data;
        }
        public BeehiveQueryServiceModel All(int currentPage, int beehivesPerPage, string userId, int beeGardenId)
        {
            var beehivesQuery = this.data.Beehives.Where(b=>b.BeeGarden.UserId==userId).AsQueryable();
            beehivesQuery = beehivesQuery.OrderBy(b => b.BeeGarden.Name).ThenByDescending(b=>b.Year);
            var totalBeehives = beehivesQuery.Count();

            var beehives = beehivesQuery
            .Skip((currentPage - 1) * beehivesPerPage)
            .Take(beehivesPerPage)
            .Select(b => new BeehiveServiceModel
            {
                Id = b.Id,
                Name = b.Name,
                Year = b.Year,
                ImageUrl = b.ImageUrl,
                Category=b.Category.Name,
                BeeGardenId=b.BeeGardenId,
                BeeGarden=b.BeeGarden.Name
            })
            .ToList();

            return new BeehiveQueryServiceModel
            {
                TotalBeehives = totalBeehives,
                CurrentPage = currentPage,
                BeehivesPerPage = beehivesPerPage,
                Beehives = beehives
            };
        }
    }
}
