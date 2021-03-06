using BeekeepingDiary.Services.Beehives;
using System.Collections.Generic;

namespace BeekeepingDiary.Models.Beehives
{
    public class AllBeehivesQueryModel
    {
        public const int BeehivesPerPage = 3;
        public int CurrentPage { get; init; } = 1;
        public int TotalBeehives { get; set; }
        public int BeeGardenId { get; set; }
        public IEnumerable<BeehiveServiceModel> Beehives { get; set; }
    }
}
