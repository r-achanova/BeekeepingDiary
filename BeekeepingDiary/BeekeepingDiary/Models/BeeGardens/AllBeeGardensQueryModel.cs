using BeekeepingDiary.Services.BeeGardens;
using System.Collections.Generic;

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

