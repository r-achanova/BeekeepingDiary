using BeekeepingDiary.Services.Statistics;
using Moq;

namespace BeekeepingDiary.Tests.Mocs
{
    public class StatisticsServiceMock
    {
        public static IStatisticsService Instance
        {
            get 
            {
                var statisticsServiceMock = new Mock<IStatisticsService>();

                statisticsServiceMock
                    .Setup(s => s.Total())
                    .Returns(new StatisticsServiceModel
                    {
                        TotalBeeGardens = 0,
                        TotalUsers = 5
                    });

                return statisticsServiceMock.Object;
            }
            
        }
    }
}
