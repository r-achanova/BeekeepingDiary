using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static BeekeepingDiary.Data.DataConstants.Produce;
using System.Linq;
using System.Threading.Tasks;

namespace BeekeepingDiary.Data.Models
{
    public class Produce
    {
        public int Id { get; init; }

        public DateTime Data { get; set; }

        public int BeehiveId { get; set; }
        public Beehive Beehive { get; set; }

        public double HoneyKg { get; set; }

        [Required]
        [MaxLength(HoneyTypeMaxLength)]
        public string HoneyType { get; set; }

        public string Notes { get; set; }
    }
}
