using BeekeepingDiary.Services.Beehives;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static BeekeepingDiary.Data.DataConstants.Beehive;

namespace BeekeepingDiary.Models.Beehives
{
    public class BeehiveFormModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; init; }
       
        [Required]
        [Url]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; init; }

        public int Year { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; init; }

        public IEnumerable<BeehiveCategoryServiceModel> Categories { get; set; }

        [Display(Name = "Bee-garden")]
        public int BeeGardenId { get; init; }

        public IEnumerable<BeehiveBeeGardenServiceModel> BeeGardens { get; set; }
    }
}
