using BeekeepingDiary.Services.BeeGardens;
using BeekeepingDiary.Services.Beehives;
using BeekeepingDiary.Services.Inspections;
using BeekeepingDiary.Tests.Mocs;
using System;
using System.Linq;
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
            var beehiveService = new BeehiveService(data);
            var inspectionService = new InspectionService(data);
            var beehiveId = CreateBeeGardenWithBeehive(beeGardenService, beehiveService);
            var inspectionId = CreateInspection(beehiveId, inspectionService);

            //Act
            var result = inspectionService.Details(
                inspectionId);

            //Assert
            Assert.Equal(Description, result.Description);
            Assert.Equal(date, result.Date);
            Assert.Equal(BeehiveId, result.BeehiveId);
        }

        [Fact]
        public void IsInspectionServiceDeleteInspectionById()
        {

            //Arrange
            var data = DatabaseMock.Instance;
            var beeGardenService = new BeeGardenService(data);
            var beehiveService = new BeehiveService(data);
            var inspectionService = new InspectionService(data);
            var beehiveId = CreateBeeGardenWithBeehive(beeGardenService, beehiveService);
            var inspectionId = CreateInspection(beehiveId, inspectionService);

            //Act
            var result = inspectionService.Delete(inspectionId);
            var currentInspection = data.Inspections.FirstOrDefault(i => i.Id == inspectionId);

            //Assert
            Assert.True(result);
            Assert.Null(currentInspection);
        }

        [Fact]
        public void InspectionServiceAllCorrect()
        {

            //Arrange
            var data = DatabaseMock.Instance;
            var beeGardenService = new BeeGardenService(data);
            var beehiveService = new BeehiveService(data);
            var inspectionService = new InspectionService(data);
            var beehiveId = CreateBeeGardenWithBeehive(beeGardenService, beehiveService);
            var inspectionId = CreateInspection(beehiveId, inspectionService);

            //Act

            var result = inspectionService.All(BeeGardenUserId, beehiveId);
            //Assert
            Assert.NotNull(result);

            Assert.IsType<InspectionQueryServiceModel>(result);

            Assert.Single(result.Inspections);
        }


        private int CreateBeeGardenWithBeehive(
            BeeGardenService beeGardenService,
            BeehiveService beehiveService)
        {
            var beeGardenId = beeGardenService.Create(
                BeeGardenName,
                BeeGardenLocation,
                BeeGardenImageUrl,
                BeeGardenYear,
                BeeGardenUserId);

            var beehiveId = beehiveService.Create(
                 Name,
                 ImageUrl,
                 Year,
                 CategoryId,
                 beeGardenId
                 );

            return beehiveId;
        }
        private int CreateInspection(
            int beehiveId,
            InspectionService inspectionService)
        {
            var inspectionId = inspectionService.Create(
                date,
                beehiveId,
                Description
                );
            return inspectionId;
        }
    }
}
