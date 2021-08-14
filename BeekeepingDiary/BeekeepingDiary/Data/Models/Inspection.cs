using System;
using System.ComponentModel.DataAnnotations;


namespace BeekeepingDiary.Data.Models
{
    public class Inspection
    {
        public int Id { get; init; }

        public DateTime Date { get; set; }
       
        public int BeehiveId { get; set; }
        public Beehive Beehive { get; set; }

        [Required]
        public string Description { get; set; }
        
    }
}
