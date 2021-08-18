using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static BeekeepingDiary.Data.DataConstants.Beehive;


namespace BeekeepingDiary.Data.Models
{
    public class Beehive
    {
        public int Id { get; init; }
        
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int BeeGardenId { get; set; }
        public BeeGarden BeeGarden { get; set; }

        [Required]
        public string ImageUrl { get; set; }
        public int Year { get; set; }
        public IEnumerable<Inspection> Inspections { get; init; } = new List<Inspection>();
        public IEnumerable<Produce> Produces { get; init; } = new List<Produce>();

    }
}
