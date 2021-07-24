using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeekeepingDiary.Data.Models
{
    public class Inspection
    {
        public int Id { get; init; }

        public DateTime Data { get; set; }
       
        public int BeehiveId { get; set; }
        public Beehive Beehive { get; set; }

        [Required]
        public string Description { get; set; }
        
    }
}
