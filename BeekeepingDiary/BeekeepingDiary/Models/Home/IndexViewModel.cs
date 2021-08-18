using System.Collections.Generic;

namespace BeekeepingDiary.Models.Home
{
    public class IndexViewModel
    {
        public int TotalBeeGardens { get; init; }
        public int TotalUsers { get; init; }
        public List<BeeGardenIndexViewModel> BeeGardens { get; init; }
    }
}
