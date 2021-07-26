using BeekeepingDiary.Data;
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

        public BeeGardenQueryServiceModel All(int currentPage, int beeGardensPerPage)
        {
            var beeGardensQuery = this.data.BeeGardens.AsQueryable();
            beeGardensQuery = beeGardensQuery.OrderByDescending(b => b.Year);
            var totalBeeGardens = beeGardensQuery.Count();

            var beeGardens = beeGardensQuery
            .Skip((currentPage - 1) * beeGardensPerPage)
            .Take(beeGardensPerPage)
            .Select(c => new BeeGardenServiceModel
            {
                Id = c.Id,
                Name = c.Name,
                Location = c.Location,
                Year = c.Year,
                ImageUrl = c.ImageUrl,
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
    }
}
