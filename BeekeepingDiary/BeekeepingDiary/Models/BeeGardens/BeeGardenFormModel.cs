using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static BeekeepingDiary.Data.DataConstants;
using static BeekeepingDiary.Data.DataConstants.BeeGarden;

namespace BeekeepingDiary.Models.BeeGardens
{
    public class BeeGardenFormModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; init; }
        
        [Required]
        [StringLength(LocationMaxLength, MinimumLength = LocationMinLength)]
        public string Location { get; init; }

        [Required]
        [Url]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; init; }
        public int Year { get; set; }
        public IEnumerable<Beehive> Beehives { get; init; } = new List<Beehive>();


    }
}
