using System.Linq;
using BeekeepingDiary.Services.BeeGardens;
using BeekeepingDiary.Services.Beehives;
using BeekeepingDiary.Tests.Mocs;
using Xunit;

namespace BeekeepingDiary.Tests.Sevices
{
    public class BeehiveServiceTest
    {
        private const string Name = "BeehiveNameTest";
        private const int CategoryId = 1;
        private const int BeeGardenId = 1;
        private const string ImageUrl = "ImageUrlTest";
        private const int Year = 2020;
        private const string NewName = "NewBeehiveNameTest";

        private const string BeeGardenName = "BeeGardenNameTest";
        private const string BeeGardenLocation = "LocationTest";
        private const string BeeGardenImageUrl = "ImageUrlTest";
        private const int BeeGardenYear = 2020;
        private const string BeeGardenUserId = "UseIdTest";
        

        [Fact]
        public void IsBeehiveServiceCreateBeehive()
        {

            //Arrange
            var data = DatabaseMock.Instance;

            var beehiveService = new BeehiveService(data);


            //Act
            var result = beehiveService.Create(
                Name,
                ImageUrl,
                Year,
                CategoryId,
                BeeGardenId
                );

            //Assert
            Assert.NotEqual(0, result);
        }

        [Fact]
        public void IsBeehiveServiceEditBeehive()
        {

            //Arrange
            var data = DatabaseMock.Instance;

            var beehiveService = new BeehiveService(data);
            var beehiveIdTest = beehiveService.Create(
                Name,
                ImageUrl,
                Year,
                CategoryId,
                BeeGardenId
                );

            //Act
            var result = beehiveService.Edit(
                beehiveIdTest,
                NewName,
                ImageUrl,
                Year,
                CategoryId,
                BeeGardenId
                );
            var beehive = data.Beehives.FirstOrDefault(b => b.Id == beehiveIdTest).Name;
            
            //Assert
            Assert.True(result);
            Assert.Equal(NewName, beehive);
        }

        [Fact]
        public void IsBeehiveServiceDetailsReturnBeehive()
        {

            //Arrange
            var data = DatabaseMock.Instance;

            var beeGardenService = new BeeGardenService(data);

            var beeGardenId = beeGardenService.Create(
                BeeGardenName,
                BeeGardenLocation,
                BeeGardenImageUrl,
                BeeGardenYear,
                BeeGardenUserId);

            var beehiveService = new BeehiveService(data);

            var beehiveIdTest = beehiveService.Create(
                 Name,
                 ImageUrl,
                 Year,
                 CategoryId,
                 beeGardenId
                 );


            //Act
            var result = beehiveService.Details(beehiveIdTest);

            //Assert
            Assert.Equal(Name, result.Name);
            Assert.Equal(ImageUrl, result.ImageUrl);
            Assert.Equal(Year, result.Year);
            Assert.Equal(CategoryId, result.CategoryId);
            Assert.Equal(BeeGardenId, result.BeeGardenId);
        }

        [Fact]
        public void IsBeehiveServiceReturnDetailsNullWhenBeehiveIdNotExists()
        {

            //Arrange
            var data = DatabaseMock.Instance;

            var beehiveService = new BeehiveService(data);

            var fakeBeehiveId = 12; // not existing beehive

            //Act
            var result = beehiveService.Details(fakeBeehiveId);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void BeehiveServiceAllCorrect()
        {

            //Arrange
            var data = DatabaseMock.Instance;

            var beeGardenService = new BeeGardenService(data);

            var beeGardenId = beeGardenService.Create(
                BeeGardenName,
                BeeGardenLocation,
                BeeGardenImageUrl,
                BeeGardenYear,
                BeeGardenUserId);

            var beehiveService = new BeehiveService(data);

            var beehiveIdOne = beehiveService.Create(
                 Name,
                 ImageUrl,
                 Year,
                 CategoryId,
                 beeGardenId
                 );

            var beehiveIdTwo = beehiveService.Create(
                 NewName,
                 ImageUrl,
                 Year,
                 CategoryId,
                 beeGardenId
                 );


            //Act

            var result = beehiveService.All(1, 1, BeeGardenUserId, beeGardenId);
            //Assert
            Assert.NotNull(result);

            Assert.IsType<BeehiveQueryServiceModel>(result);

            Assert.Single(result.Beehives);

            Assert.Equal(2, result.TotalBeehives);
        }
    }
}
