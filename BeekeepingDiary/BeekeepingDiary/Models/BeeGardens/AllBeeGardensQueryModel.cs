using System.Collections.Generic;
using BeekeepingDiary.Services.BeeGardens;

namespace BeekeepingDiary.Models.BeeGardens
{
    public class AllBeeGardensQueryModel
    {
        public const int BeeGardensPerPage = 3;

        public int CurrentPage { get; init; } = 1;

        public int TotalBeeGardens { get; set; }

        public IEnumerable<BeeGardenServiceModel> BeeGardens { get; set; }
    }
}

