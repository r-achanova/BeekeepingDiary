using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeekeepingDiary.Models.BeeGardens
{
    public class BeeGardenListingViewModel
    {
        public int Id { get; init; }

        public string Name { get; set; }
        
        public string Location { get; set; }

        public string ImageUrl { get; set; }

        public int Year { get; set; }
        
    }
}
