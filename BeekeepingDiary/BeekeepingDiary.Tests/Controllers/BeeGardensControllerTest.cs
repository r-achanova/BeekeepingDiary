using BeekeepingDiary.Controllers;
using MyTested.AspNetCore.Mvc;
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
    }
}
