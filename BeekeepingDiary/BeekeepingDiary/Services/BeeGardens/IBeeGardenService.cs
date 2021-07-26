using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeekeepingDiary.Services.BeeGardens
{
    public interface IBeeGardenService
    {
        BeeGardenQueryServiceModel All(
            int currentPage,
            int beeGardensPerPage);
        
    }
}
