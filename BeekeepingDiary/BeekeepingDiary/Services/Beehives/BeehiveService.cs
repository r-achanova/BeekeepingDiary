using BeekeepingDiary.Data;
using BeekeepingDiary.Data.Models;
using BeekeepingDiary.Services.Beehives;
using BeekeepingDiary.Models.Beehives;
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
            var beehivesQuery = this.data.Beehives.Where(b => b.BeeGarden.UserId == userId).AsQueryable();
            beehivesQuery = beehivesQuery.OrderBy(b => b.BeeGarden.Name).ThenByDescending(b => b.Year);
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
                Category = b.Category.Name,
                // BeeGardenId=b.BeeGardenId,
                BeeGarden = b.BeeGarden.Name
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

        public IEnumerable<BeehiveBeeGardenServiceModel> AllBeeGardens(string userId)
        {

            var beeGardens = this.data
                  .BeeGardens
                  .Where(b => b.UserId == userId)
                  .Select(b => new BeehiveBeeGardenServiceModel
                  {
                      Id = b.Id,
                      Name = b.Name,
                      UserId = b.UserId
                  })
                   .ToList();
            return beeGardens;
        }

        public IEnumerable<BeehiveCategoryServiceModel> AllCategories()
        => this.data
                .Categories
                .Select(c => new BeehiveCategoryServiceModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();

        public int Create(string name, string imageUrl, int year, int categoryId, int beeGardenId)
        {
            var beehiveData = new Beehive
            {
                Name = name,
                ImageUrl = imageUrl,
                Year = year,
                CategoryId = categoryId,
                BeeGardenId = beeGardenId
            };
            this.data.Beehives.Add(beehiveData);
            this.data.SaveChanges();

            return beehiveData.Id;
        }

        public bool IsInBeeGardenId(int beehiveId, int beeGardenId)
        {
            var beehiveBeeGardenId = this.data.Beehives
                .Where(x => x.Id == beehiveId)
                .Select(x => x.BeeGardenId)
                .FirstOrDefault();
            return beehiveBeeGardenId == beeGardenId;
        }
    }
}
