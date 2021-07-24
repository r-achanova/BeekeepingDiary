using BeekeepingDiary.Data;
using BeekeepingDiary.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeekeepingDiary.Infrastructure
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<BeekeepingDbContext>();

            data.Database.Migrate();

            SeedCategories(data);

            return app;
        }

        private static void SeedCategories(BeekeepingDbContext data)
        {
            if (data.Categories.Any())
            {
                return;
            }

            data.Categories.AddRange(new[]
            {
                new Category { Name = "Дадан Блат 12" },
                new Category { Name = "Дадан Блат 10" },
                new Category { Name = "Дадан Блат 8" },
                new Category { Name = "Дадан Блат 6" },
                new Category { Name = "Многокорпусен 10" },
                new Category { Name = "Хоризонтален 24" },
                new Category { Name = "Хоризонтален 32" },
                new Category { Name = "Алпийски /фарар/" },
            });

            data.SaveChanges();
        }
    }
}
