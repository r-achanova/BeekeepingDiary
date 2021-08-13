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
            var beehivesQuery = GetBeehivesByBeeGardenId(beeGardenId);
            beehivesQuery = beehivesQuery.OrderBy(b => b.Name).ThenByDescending(b => b.Year);
            var totalBeehives = beehivesQuery.Count();

            var beehives = beehivesQuery
            .Skip((currentPage - 1) * beehivesPerPage)
            .Take(beehivesPerPage)
            .ToList();

            return new BeehiveQueryServiceModel
            {
                TotalBeehives = totalBeehives,
                CurrentPage = currentPage,
                BeehivesPerPage = beehivesPerPage,
                Beehives = beehives
            };
        }

        public BeehiveQueryServiceModel Mine(int currentPage, int beehivesPerPage, string userId)
        {
            var beehivesQuery = GetBeehivesByUserId(userId);
            beehivesQuery = beehivesQuery.OrderBy(b => b.BeeGarden).ThenBy(b => b.Name);
            var totalBeehives = beehivesQuery.Count();

            var beehives = beehivesQuery
            .Skip((currentPage - 1) * beehivesPerPage)
            .Take(beehivesPerPage)
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

        public BeehiveDetailsServiceModel Details(int id)
            => this.data
                .Beehives
                .Where(c => c.Id == id)
                .Select(c => new BeehiveDetailsServiceModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Year = c.Year,
                    ImageUrl = c.ImageUrl,
                    CategoryId=c.CategoryId,
                    Category = c.Category.Name,
                    BeeGardenId=c.BeeGardenId,
                    BeeGarden = c.BeeGarden.Name,
                    UserId = c.BeeGarden.UserId
                })
                .FirstOrDefault();
        public bool Edit(int beehiveId, string name, string imageUrl, int year, int categoryId, int beeGardenId)
        {
            var beehiveData = this.data.Beehives.Find(beehiveId);
            if (beehiveData == null)
            {
                return false;
            }

            beehiveData.Name = name;
            beehiveData.Year = year;
            beehiveData.ImageUrl = imageUrl;
            beehiveData.CategoryId = categoryId;
           beehiveData.BeeGardenId = beeGardenId;

            this.data.SaveChanges();

            return true;

        }

        public IEnumerable<BeehiveServiceModel> GetBeehivesByBeeGardenId(int beeGardenId)
        {
            var query = this.data.Beehives
                .Where(x => x.BeeGardenId == beeGardenId)
                .Select(b => new BeehiveServiceModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    ImageUrl = b.ImageUrl,
                    Year = b.Year,
                    Category = b.Category.Name,
                    BeeGarden = b.BeeGarden.Name
                })
                .ToList();
            return query;
        }

        public IEnumerable<BeehiveServiceModel> GetBeehivesByUserId(string userId)
        {
            var query = this.data.Beehives
                .Where(b => b.BeeGarden.UserId == userId)
                .Select(b => new BeehiveServiceModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    ImageUrl = b.ImageUrl,
                    Year = b.Year,
                    Category = b.Category.Name,
                    BeeGarden = b.BeeGarden.Name
                })
                .ToList();
            return query;
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
