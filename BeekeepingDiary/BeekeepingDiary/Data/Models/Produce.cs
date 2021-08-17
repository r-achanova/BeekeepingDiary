using System;
using System.ComponentModel.DataAnnotations;
using static BeekeepingDiary.Data.DataConstants.Produce;


namespace BeekeepingDiary.Data.Models
{
    public class Produce
    {
        public int Id { get; init; }

        public DateTime Date { get; set; }

        public int BeehiveId { get; set; }
        public Beehive Beehive { get; set; }

        [Required]
        [Range(HoneyTypeMinKg, HoneyTypeMaxKg)]
        public double HoneyKg { get; set; }

        [Required]
        [MaxLength(HoneyTypeMaxLength)]
        public string HoneyType { get; set; }

        public string Notes { get; set; }
    }
}
