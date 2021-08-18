using System.Collections.Generic;

namespace BeekeepingDiary.Services.BeeGardens
{
    public class IndexServiceModel
    {
        public int TotalBeeGardens { get; init; }

        public int TotalUsers { get; init; }

        public List<BeeGardenServiceModel> BeeGardens { get; init; }
    }
}
