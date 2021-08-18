using BeekeepingDiary.Data;
using BeekeepingDiary.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;

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

                var context = new BeekeepingDbContext(dbContextOptions);
                // seed DB categories
                var category = context.Categories.Add(new Category { Name = "Дадан Блат 12" });
                context.SaveChanges();
                return context;
            }
        }
    }
}
