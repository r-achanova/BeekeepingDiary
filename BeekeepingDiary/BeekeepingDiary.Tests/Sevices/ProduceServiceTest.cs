using System;
using System.Linq;
using BeekeepingDiary.Services.BeeGardens;
using BeekeepingDiary.Services.Beehives;
using BeekeepingDiary.Services.Produces;
using BeekeepingDiary.Tests.Mocs;
using Xunit;

namespace BeekeepingDiary.Tests.Sevices
{
   public class ProduceServiceTest
    {
        private DateTime date = DateTime.Now;
        private const int BeehiveId = 1;
        private const int HoneyKg = 1;
        private const string HoneyType = "HoneyTypeTes";
        private const string Notes = "NotesTest";
        private const int NewHoneyKg = 100;

        private const string Name = "BeehiveNameTest";
        private const int CategoryId = 1;
        private const string ImageUrl = "ImageUrlTest";
        private const int Year = 2020;
        
        private const string BeeGardenName = "BeeGardenNameTest";
        private const string BeeGardenLocation = "LocationTest";
        private const string BeeGardenImageUrl = "ImageUrlTest";
        private const int BeeGardenYear = 2020;
        private const string BeeGardenUserId = "UseIdTest";

        [Fact]
        public void IsProduceServiceCreateProduce()
        {

            //Arrange
            var data = DatabaseMock.Instance;

            var produceService = new ProduceService(data);


            //Act
            var result = produceService.Create(
                date,
                BeehiveId,
                HoneyKg,
                HoneyType,
                Notes
                );
            var currentProduce = data.Produces.FirstOrDefault(i => i.Id == result);

            //Assert
            Assert.NotEqual(0, result);
            Assert.Equal(BeehiveId, currentProduce.BeehiveId);
            Assert.Equal(date, currentProduce.Date);
            Assert.Equal(HoneyKg, currentProduce.HoneyKg);
            Assert.Equal(HoneyType, currentProduce.HoneyType);
            Assert.Equal(Notes, currentProduce.Notes);
        }

        [Fact]
        public void IsProduceServiceEditProduce()
        {

            //Arrange
            var data = DatabaseMock.Instance;

            var produceService = new ProduceService(data);

            var produceIdTest = produceService.Create(
                date,
                BeehiveId,
                HoneyKg,
                HoneyType,
                Notes
                );
            var currentProduce = data.Produces.FirstOrDefault(i => i.Id == produceIdTest);

            //Act
            var result = produceService.Edit(
                produceIdTest,
                date,
                NewHoneyKg,
                HoneyType,
                Notes
                );
            var produceHoheyKg = data.Produces.FirstOrDefault(i => i.Id == produceIdTest).HoneyKg;
            
            //Assert
            Assert.True(result);
            Assert.Equal(NewHoneyKg, produceHoheyKg);
        }

        [Fact]
        public void IsInspectionServiceReturnDetailsInspection()
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

            var beehiveId = beehiveService.Create(
                 Name,
                 ImageUrl,
                 Year,
                 CategoryId,
                 beeGardenId
                 );

            var produceService = new ProduceService(data);
            var produceIdTest = produceService.Create(
                date,
                BeehiveId,
                HoneyKg,
                HoneyType,
                Notes
                );
           
            //Act
            var result = produceService.Details(
                produceIdTest);

            //Assert
            Assert.Equal(date, result.Date);
            Assert.Equal(BeehiveId, result.BeehiveId); 
            Assert.Equal(HoneyKg, result.HoneyKg); 
            Assert.Equal(HoneyType, result.HoneyType); 
            Assert.Equal(Notes, result.Notes);
        }
    }
}
