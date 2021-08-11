using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeekeepingDiary.Services.Beehives
{
    public class BeehiveServiceModel
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string ImageUrl { get; init; }
        public int Year { get; init; }
        public string Category { get; init; }
        public int BeeGardenId { get; set; }
        public string BeeGarden { get; init; }
    }
}
