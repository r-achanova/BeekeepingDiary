using System.Collections.Generic;

namespace BeekeepingDiary.Services.BeeGardens
{
    public class BeeGardenQueryServiceModel
    {
        public int CurrentPage { get; init; }

        public int BeeGardensPerPage { get; init; }

        public int TotalBeeGardens { get; init; }

        public IEnumerable<BeeGardenServiceModel> BeeGardens { get; init; }
    }
}
