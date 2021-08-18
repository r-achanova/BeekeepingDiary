using System;
using System.Linq;
using BeekeepingDiary.Services.BeeGardens;
using BeekeepingDiary.Services.Beehives;
using BeekeepingDiary.Services.Inspections;
using BeekeepingDiary.Tests.Mocs;

using Xunit;

namespace BeekeepingDiary.Tests.Sevices
{
    public class InspectionServiceTest
    {

        private DateTime date = DateTime.Now;
        private const int BeehiveId = 1;
        private const string Description = "DescriptionTest";
        private const string NewDescription = "NewDescriptionTest";
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
        public void IsInspectionServiceCreateInspection()
        {

            //Arrange
            var data = DatabaseMock.Instance;

            var inspectionService = new InspectionService(data);


            //Act
            var result = inspectionService.Create(
                date,
                BeehiveId,
                Description
                );
            var currentInspection = data.Inspections.FirstOrDefault(i => i.Id == result);

            //Assert
            Assert.NotEqual(0, result);
            Assert.Equal(Description, currentInspection.Description);
            Assert.Equal(date, currentInspection.Date);
            Assert.Equal(BeehiveId, currentInspection.BeehiveId);
        }

        [Fact]
        public void IsInspectionServiceEditInspection()
        {

            //Arrange
            var data = DatabaseMock.Instance;

            var inspectionService = new InspectionService(data);
            var inspectionIdTest = inspectionService.Create(
                date,
                BeehiveId,
                Description
                );

            //Act
            var result = inspectionService.Edit(
                inspectionIdTest,
                date,
                NewDescription
                );
            var inspectionDescription = data.Inspections.FirstOrDefault(i => i.Id == inspectionIdTest).Description;
            //Assert
            Assert.True(result);
            Assert.Equal(NewDescription, inspectionDescription);
        }

        [Fact]
        public void IsInspectionServiceDetailsReturnInspection()
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
            var inspectionService = new InspectionService(data);
            var inspectionIdTest = inspectionService.Create(
                date,
                beehiveId,
                Description
                );

            //Act
            var result = inspectionService.Details(
                inspectionIdTest);

            //Assert
            Assert.Equal(Description, result.Description);
            Assert.Equal(date, result.Date);
            Assert.Equal(BeehiveId, result.BeehiveId);
        }
    }
}
