using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace BeekeepingDiary.Data.Models
{
    public class ApplicationUser: IdentityUser
    {
        public IEnumerable<BeeGarden> BeeGardens { get; init; } = new List<BeeGarden>();
    }
}
