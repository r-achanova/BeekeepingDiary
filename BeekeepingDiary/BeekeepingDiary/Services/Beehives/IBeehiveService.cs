using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeekeepingDiary.Services.Beehives
{
   public interface IBeehiveService
    {
        BeehiveQueryServiceModel All(
            int currentPage,
            int beehivesPerPage,
            string userId,
            int beeGardenId
            );

        bool IsInBeeGardenId(int beehiveId, int beeGardenId);

        int Create(
            string name,
            string imageUrl,
            int year,
            int categoryId,
            int beeGardenId);

        IEnumerable<BeehiveCategoryServiceModel> AllCategories();
        IEnumerable<BeehiveBeeGardenServiceModel> AllBeeGardens(string userId);
        public IEnumerable<BeehiveServiceModel> GetBeehivesByUserId(string userId);

        public IEnumerable<BeehiveServiceModel> GetBeehivesByBeeGardenId(int beeGardenId);
        public BeehiveQueryServiceModel Mine(
            int currentPage,
            int beehivesPerPage,
            string userId);
    }
}
