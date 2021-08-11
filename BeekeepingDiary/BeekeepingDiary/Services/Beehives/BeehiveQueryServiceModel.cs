using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeekeepingDiary.Services.Beehives
{
    public class BeehiveQueryServiceModel
    {
        public int CurrentPage { get; init; }
        public int BeehivesPerPage { get; init; }
        public int TotalBeehives { get; init; }
         public IEnumerable<BeehiveServiceModel> Beehives { get; init; }
    }
}
