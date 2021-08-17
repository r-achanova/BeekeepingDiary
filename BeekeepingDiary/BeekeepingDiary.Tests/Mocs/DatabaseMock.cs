using BeekeepingDiary.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeekeepingDiary.Tests.Mocs
{
  public static class DatabaseMock
    {
        public static BeekeepingDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<BeekeepingDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new BeekeepingDbContext(dbContextOptions);
            }
        }
    }
}
