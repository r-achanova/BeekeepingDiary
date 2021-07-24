using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static BeekeepingDiary.Data.DataConstants.BeeGarden;


namespace BeekeepingDiary.Data.Models
{
    public class BeeGarden
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }
        public virtual ApplicationUser ApplicationUser { get; init; }

        [Required]
        [MaxLength(LocationMaxLength)]
        public string Location { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public int Year { get; set; }
        public IEnumerable<Beehive> Beehives { get; init; } = new List<Beehive>();



    }
}
