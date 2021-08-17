using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BeekeepingDiary.Services.Produces;
using static BeekeepingDiary.Data.DataConstants.Produce;
using System.Linq;
using System.Threading.Tasks;

namespace BeekeepingDiary.Models.Produces
{
    public class ProduceFormModel
    {
        public int Id { get; init; }

        public DateTime Date { get; set; }

        public int BeehiveId { get; set; }
        public IEnumerable<ProduceBeehiveServiceModel> Beehives { get; set; }
        public string BeehiveName { get; set; }

        [Required(ErrorMessage = "Въведете количество")]
        [Range(HoneyTypeMinKg, HoneyTypeMaxKg, ErrorMessage = "Количеството трябва да бъде между 0 и 200 кг.")]
        public double HoneyKg { get; set; }

        [Required(ErrorMessage = "Въведете тип мед.")]
        [StringLength(HoneyTypeMaxLength, MinimumLength = HoneyTypeMinLength)]
        public string HoneyType { get; set; }

        public string Notes { get; set; }
    }
}
