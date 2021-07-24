using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static BeekeepingDiary.Data.DataConstants.Produce;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BeekeepingDiary.Data.Models
{
    public class ApplicationUser: IdentityUser
    {
        public IEnumerable<BeeGarden> BeeGardens { get; init; } = new List<BeeGarden>();
    }
}
