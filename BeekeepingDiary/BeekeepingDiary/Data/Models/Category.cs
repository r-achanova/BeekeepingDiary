using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeekeepingDiary.Data.Models
{
    public class Category
    {
        public int Id { get; init; }
        public string Name { get; set; }
        public IEnumerable<Beehive> Beehives { get; init; } = new List<Beehive>();
    }
}
