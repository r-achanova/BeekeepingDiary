using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeekeepingDiary.Services.BeeGardens
{
    public class IndexServiceModel
    {
        public int TotalBeeGardens { get; init; }

        public int TotalUsers { get; init; }

        public List<BeeGardenServiceModel> BeeGardens { get; init; }
    }
}
