using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeekeepingDiary.Models.Home
{
    public class IndexViewModel
    {
        public int TotalBeeGardens { get; init; }

        public int TotalUsers { get; init; }

        public List<BeeGardenIndexViewModel> BeeGardens { get; init; }
    }
}
