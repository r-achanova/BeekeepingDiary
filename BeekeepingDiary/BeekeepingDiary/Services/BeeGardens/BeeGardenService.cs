using System.Linq;
using System.Collections.Generic;
using BeekeepingDiary.Data;
using BeekeepingDiary.Data.Models;

namespace BeekeepingDiary.Services.BeeGardens
{
    public class BeeGardenService : IBeeGardenService
    {
        private readonly BeekeepingDbContext data;
        public BeeGardenService(BeekeepingDbContext data)
        {
            this.data = data;
        }

        public BeeGardenQueryServiceModel All()
        {
            var beeGardensQuery = this.data.BeeGardens.AsQueryable();
            beeGardensQuery = beeGardensQuery.OrderByDescending(b => b.Year);
            var totalBeeGardens = beeGardensQuery.Count();

            var beeGardens = beeGardensQuery
            .Select(b => new BeeGardenServiceModel
            {
                Id = b.Id,
                Name = b.Name,
                Location = b.Location,
                Year = b.Year,
                ImageUrl = b.ImageUrl,
                UserId = b.ApplicationUser.UserName // todo update
            })
            .ToList();

            return new BeeGardenQueryServiceModel
            {
                TotalBeeGardens = totalBeeGardens,
                BeeGardens = beeGardens
            };
        }

        public BeeGardenQueryServiceModel AllForUser(int currentPage, int beeGardensPerPage, string userId)
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

        public IEnumerable<BeeGardenServiceModel> Index()
        {
            var beeGardens = this.data
                .BeeGardens
                .OrderBy(b => b.Id)
                .Select(b => new BeeGardenServiceModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    Location = b.Location,
                    Year = b.Year,
                    ImageUrl = b.ImageUrl
                })
                .ToList();
            return beeGardens;
        }
        public int Create(string name, string location, string imageUrl, int year, string userId)
        {
            var beeGardenData = new BeeGarden
            {
                Name = name,
                Location = location,
                ImageUrl = imageUrl,
                Year = year,
                UserId = userId,
            };

            this.data.BeeGardens.Add(beeGardenData);
            this.data.SaveChanges();

            return beeGardenData.Id;
        }

        public BeeGardenServiceModel Details(int id)
        => this.data
                .BeeGardens
                .Where(b => b.Id == id)
                .Select(b => new BeeGardenServiceModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    Location = b.Location,
                    Year = b.Year,
                    ImageUrl = b.ImageUrl,
                    UserId=b.UserId
                })
                .FirstOrDefault();

        public bool Edit(int beeGardenId, string name, string location, string imageUrl, int year)
        {
            var beeGardenData = this.data.BeeGardens.Find(beeGardenId);
            if (beeGardenData == null)
            {
                return false;
            }

            beeGardenData.Name = name;
            beeGardenData.Location = location;
            beeGardenData.Year = year;
            beeGardenData.ImageUrl = imageUrl;

            this.data.SaveChanges();

            return true;
        }

       public bool IsByCurrentUser(int beeGardenId, string userId)
        => this.data
                .BeeGardens
                .Any(b => b.Id == beeGardenId && b.UserId == userId);
      
    }
    }


