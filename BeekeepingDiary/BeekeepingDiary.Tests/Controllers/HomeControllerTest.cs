using BeekeepingDiary.Controllers;
using BeekeepingDiary.Services.BeeGardens;
using BeekeepingDiary.Tests.Mocs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
     
        public void HomeControllerShouldReturnIndexServiceModel()
        {
            // Arrange
            var homeController = new HomeController(StatisticsServiceMock.Instance, null);

            // Act
            var result = homeController.Index();

            // Assert
           // Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
           // Assert.Equal(0, result.TotalBeeGardens());
           // Assert.Equal(5, result.TotalUsers);
           // Assert.Equal(null, result.BeeGardens);



           
        }
    }
}
