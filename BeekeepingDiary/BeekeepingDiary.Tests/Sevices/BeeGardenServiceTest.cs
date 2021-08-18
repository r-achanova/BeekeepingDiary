using System.Linq;
using BeekeepingDiary.Services.BeeGardens;
using BeekeepingDiary.Tests.Mocs;
using Xunit;

namespace BeekeepingDiary.Tests.Sevices
{
    public class BeeGardenServiceTest
    {
        private const string BeeGardenName = "BeeGardenNameTest";
        private const string BeeGardenLocation = "LocationTest";
        private const string BeeGardenImageUrl = "ImageUrlTest";
        private const int BeeGardenYear = 2020;
        private const string BeeGardenUserId = "UseIdTest";
        private const string NewBeeGardenName = "NewBeeGardenNameTest";

        [Fact]
        public void IsBeeGardenServiceCreateBeeGarden()
        {

            //Arrange
            var data = DatabaseMock.Instance;

            var beeGardenService = new BeeGardenService(data);

            //Act
            var result = beeGardenService.Create(
                BeeGardenName,
                BeeGardenLocation,
                BeeGardenImageUrl,
                BeeGardenYear,
                BeeGardenUserId);

            //Assert
            Assert.NotEqual(0, result);
        }

        [Fact]
        public void IsBeeGardenServiceEditBeeGarden()
        {

            //Arrange
            var data = DatabaseMock.Instance;

            var beeGardenService = new BeeGardenService(data);
            var beeGardenIdTest = beeGardenService.Create(
                BeeGardenName,
                BeeGardenLocation,
                BeeGardenImageUrl,
                BeeGardenYear,
                BeeGardenUserId);

            //Act

            var result = beeGardenService.Edit(
                beeGardenIdTest,
                NewBeeGardenName,
                BeeGardenLocation,
                BeeGardenImageUrl,
                BeeGardenYear);
            var beeGarden = data.BeeGardens.FirstOrDefault(b => b.Id == beeGardenIdTest).Name;
            
            //Assert
            Assert.True(result);
            Assert.Equal(NewBeeGardenName, beeGarden);
        }

        [Fact]
        public void IsBeeGardenServiceDetailsReturnBeeGarden()
        {

            //Arrange
            var data = DatabaseMock.Instance;
            var beeGardenService = new BeeGardenService(data);

            var beeGardenIdTest = beeGardenService.Create(
                BeeGardenName,
                BeeGardenLocation,
                BeeGardenImageUrl,
                BeeGardenYear,
                BeeGardenUserId);


            //Act
            var result = beeGardenService.Details(beeGardenIdTest);

            //Assert
            Assert.Equal(BeeGardenName, result.Name);
            Assert.Equal(BeeGardenLocation, result.Location);
            Assert.Equal(BeeGardenImageUrl, result.ImageUrl);
            Assert.Equal(BeeGardenYear, result.Year);
            Assert.Equal(BeeGardenUserId, result.UserId);
        }

    }
}
