using BeekeepingDiary.Controllers;
using BeekeepingDiary.Data.Models;
using BeekeepingDiary.Models.BeeGardens;
using BeekeepingDiary.Models.Beehives;
using BeekeepingDiary.Services.BeeGardens;
using BeekeepingDiary.Tests.Mocs;
using Microsoft.AspNetCore.Mvc;
using MyTested.AspNetCore.Mvc;
using System.Linq;
using Xunit;


namespace BeekeepingDiary.Tests.Controllers
{
   public class BeeGardensControllerTest
    {
        [Fact]
        public void DetailsShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/BeeGardens/Details/1")
                .To<BeeGardensController>(c => c.Details(1));

        [Fact]
        public void AddShouldBeForAuthorizedUsersAndReturnView()
            => MyController<BeeGardensController>
                .Instance()
                .Calling(c => c.Add())
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();

        [Fact]
        public void RouteTestAdd()
            => MyRouting
            .Configuration()
            .ShouldMap("/BeeGardens/Add")
            .To<BeeGardensController>(x => x.Add());

        [Fact]
        public void AddRoutingTestAndShouldBeForAuthorizedUsersAndReturnView()
            => MyMvc
            .Pipeline()
            .ShouldMap(request=>request
               .WithPath("/BeeGardens/Add")
            .WithUser())
            .To<BeeGardensController>(x => x.Add())
            .Which()
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                 .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .View();

        [Fact]
        public void PostAddBeeGardenShouldBeMapped()
            => MyRouting
                .Configuration().ShouldMap(request => request
                                       .WithPath("/BeeGardens/Add")
                                       .WithMethod(HttpMethod.Post))
                .To<BeeGardensController>(c => c.Add(With.Any<BeeGardenFormModel>()));

        [Theory]
        [InlineData("BeeGarden-1", "Pernik", "http://ImageUrlTest", 2012)]
        public void PostAddBeeGardenShouldBeOnlyForAuthorizedUsers(string name, string location, string imageUrl, int year)
            => MyController<BeeGardensController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(c => c.Add(new BeeGardenFormModel
                {
                    Name = name,
                    Location = location,
                    ImageUrl = imageUrl,
                    Year = year
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .Data(data => data
                    .WithSet<BeeGarden>(beeGardens => beeGardens
                        .Any(x =>
                            x.Name == name &&
                            x.Location == location &&
                            x.ImageUrl == imageUrl &&
                            x.Year == year &&
                            x.UserId == TestUser.Identifier)));


    }
}
