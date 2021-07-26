using BeekeepingDiary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeekeepingDiary.Services.Statistics
{
    public class StatisticsService:IStatisticsService
    {
        private readonly BeekeepingDbContext data;

        public StatisticsService(BeekeepingDbContext data)
        {
            this.data = data;
        }

        public StatisticsServiceModel Total()
        {
            var totalBeeGardens = this.data.BeeGardens.Count();
            var totalUsers = this.data.Users.Count();

            return new StatisticsServiceModel
            {
                TotalBeeGardens = totalBeeGardens,
                TotalUsers = totalUsers
            };
        }
    }
}
