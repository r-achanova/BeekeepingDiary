using System.Collections.Generic;

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

        public bool Edit(int beehiveId, string name, string imageUrl, int year, int categoryId, int beeGardenId);
        IEnumerable<BeehiveCategoryServiceModel> AllCategories();
        IEnumerable<BeehiveBeeGardenServiceModel> AllBeeGardens(string userId);
        public IEnumerable<BeehiveServiceModel> GetBeehivesByUserId(string userId);

        public IEnumerable<BeehiveServiceModel> GetBeehivesByBeeGardenId(int beeGardenId);
        public BeehiveQueryServiceModel Mine(
            int currentPage,
            int beehivesPerPage,
            string userId);

        public BeehiveDetailsServiceModel Details(int beehiveId);
        public string GetBeehiveName(int beehiveId);
    }
}
