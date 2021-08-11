using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        
    }
}
