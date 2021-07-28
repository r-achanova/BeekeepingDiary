using BeekeepingDiary.Data;
using BeekeepingDiary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeekeepingDiary.Services.BeeGardens
{
    public class BeeGardenService : IBeeGardenService
    {
        private readonly BeekeepingDbContext data;
        public BeeGardenService(BeekeepingDbContext data)
        {
            this.data = data;
        }

        public BeeGardenQueryServiceModel All(int currentPage, int beeGardensPerPage, string userId)
        {
            var beeGardensQuery = this.data.BeeGardens.Where(b => b.UserId == userId).AsQueryable();
            beeGardensQuery = beeGardensQuery.OrderByDescending(b => b.Year);
            var totalBeeGardens = beeGardensQuery.Count();

            var beeGardens = beeGardensQuery
            .Skip((currentPage - 1) * beeGardensPerPage)
            .Take(beeGardensPerPage)
            .Select(b => new BeeGardenServiceModel
            {
                Id = b.Id,
                Name = b.Name,
                Location = b.Location,
                Year = b.Year,
                ImageUrl = b.ImageUrl,
            })
            .ToList();

            return new BeeGardenQueryServiceModel
            {
                TotalBeeGardens = totalBeeGardens,
                CurrentPage = currentPage,
                BeeGardensPerPage = beeGardensPerPage,
                BeeGardens = beeGardens
            };
        }

       /* public IEnumerable<BeeGardenServiceModel> ByUser(string userId)
        {
            var beeGardensQuery = this.data.BeeGardens.AsQueryable();
            beeGardensQuery = beeGardensQuery.OrderByDescending(b => b.Year);


            var beeGardens = beeGardensQuery
            .Select(c => new BeeGardenServiceModel
            {
                Id = c.Id,
                Name = c.Name,
                Location = c.Location,
                Year = c.Year,
                ImageUrl = c.ImageUrl,
            }).Where(b => b.UserId == userId)
            .ToList();
            return beeGardens;
        }*/
    }
}

