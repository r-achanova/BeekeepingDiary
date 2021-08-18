using BeekeepingDiary.Data.Models;
using BeekeepingDiary.Services.Statistics;
using BeekeepingDiary.Tests.Mocs;
using System.Linq;
using Xunit;

namespace BeekeepingDiary.Tests.Sevices
{
    public class StatisticsServiceTest
    {
        private const string BeeGardenName = "BeeGardenNameTest";
        private const string BeeGardenLocation = "LocationTest";
        private const string BeeGardenImageUrl = "ImageUrlTest";
        private const int BeeGardenYear = 2020;
        private const string BeeGardenUserId = "UseIdTest";
        private const string UserName = "UseNameTest";
        private const string UserPassword = "UserPasswordTest";
        
       
        [Fact]
        public void IsStatisticsServiceTotalCorrect()
        {

            //Arrange
            var data = DatabaseMock.Instance;
           
           data.BeeGardens.Add(new BeeGarden { Name = BeeGardenName, UserId=BeeGardenUserId });
            data.SaveChanges();

           var statisticsService = new StatisticsService(data);
            var expectedUsersCount = data.Users.Count();
            var expectedBeeGardensCount = data.BeeGardens.Count();

            //Act
            var result = statisticsService.Total();
           
            //Assert
            Assert.Equal(expectedUsersCount,result.TotalUsers);
            Assert.Equal(expectedBeeGardensCount,result.TotalBeeGardens);
        }
    }
}
