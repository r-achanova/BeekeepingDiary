using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static BeekeepingDiary.Data.DataConstants.Category;

namespace BeekeepingDiary.Data.Models
{
    public class Category
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)] 
        public string Name { get; set; }
        public IEnumerable<Beehive> Beehives { get; init; } = new List<Beehive>();
    }
}
