using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static BeekeepingDiary.Data.DataConstants.Beehive;
using System.Linq;
using System.Threading.Tasks;
using BeekeepingDiary.Data.Models;

namespace BeekeepingDiary.Models.Beehive
{
    public class AddBeehiveFormModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; init; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int BeeGardenId { get; set; }
        public BeeGarden BeeGarden { get; set; }

        [Required]
        [Url]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; init; }

        public int Year { get; set; }
        public IEnumerable<Inspection> Inspections { get; init; } = new List<Inspection>();
        public IEnumerable<Produce> Produces { get; init; } = new List<Produce>();
    }
}
