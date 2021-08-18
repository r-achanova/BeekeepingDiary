using BeekeepingDiary.Controllers;
using BeekeepingDiary.Data.Models;
using BeekeepingDiary.Services.BeeGardens;
using BeekeepingDiary.Services.Statistics;
using BeekeepingDiary.Tests.Mocs;
using Microsoft.AspNetCore.Mvc;
using MyTested.AspNetCore.Mvc;
using System.Linq;
using Xunit;

namespace BeekeepingDiary.Tests.Controllers
{
    public class HomeControllerTest
    {
        [Fact]
        public void ErrorShouldReturnView()
        {
            //Arrange
            var homeController = new HomeController(null, null);

            //Act
            var result = homeController.Error();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        
        [Fact]
        public void ErrorActionShouldReturnView()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Error())
                .ShouldReturn()
                .View();

        [Fact]
        public void ErrorRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Home/Error")
                .To<HomeController>(c => c.Error());

        [Fact]
        public void IndexRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/")
                .To<HomeController>(c => c.Index());

        [Fact]
        public void IndexActionShouldReturnView()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Index())
                .ShouldReturn()
                .View();

        [Fact]
        public void IndexShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/")
                .To<HomeController>(c => c.Index());

        [Fact]
        public void IndexShouldReturnViewWithCorrectModel()
        {
            // Arrange
            var data = DatabaseMock.Instance;

            var beeGardens = Enumerable.Range(0, 10)
                .Select(x => new BeeGarden());

            data.BeeGardens
            .AddRange(beeGardens);
            data.Users.Add(new Data.Models.ApplicationUser());
            data.SaveChanges();

            var beeGardenService = new BeeGardenService(data);
            var statisticsService = new StatisticsService(data);
            var homeController = new HomeController(statisticsService, beeGardenService);

            // Act
            var result = homeController.Index();

            // Assert
            Assert.NotNull(result);

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = viewResult.Model;

            var indexViewModel = Assert.IsType<IndexServiceModel>(model);

            Assert.Equal(10, indexViewModel.BeeGardens.Count());
            Assert.Equal(10, indexViewModel.TotalBeeGardens);
            Assert.Equal(1, indexViewModel.TotalUsers);
        }
    }
}
