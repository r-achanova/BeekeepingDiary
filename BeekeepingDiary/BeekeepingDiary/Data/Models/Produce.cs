using System;
using System.ComponentModel.DataAnnotations;
using static BeekeepingDiary.Data.DataConstants.Produce;


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
