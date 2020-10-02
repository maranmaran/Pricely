using DataAccess.NoSql.Interfaces;
using MenuService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenuService.Persistence.Seed
{
    public class DbSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            var logger = services.GetService<ILogger<DbSeeder>>();
            var menuContext = services.GetService<IGenericDocumentRepository<Menu>>();

            try
            {
                await SeedMenus(menuContext, logger);
            }
            catch (Exception e)
            {
                logger.LogError($"{e.Message} {e.InnerException?.Message}");
                throw;
            }
        }

        static async Task SeedMenus(IGenericDocumentRepository<Menu> context, ILogger<DbSeeder> logger)
        {
            var data = await context.AsQueryable().ToListAsync();
            var hasData = data.Any();

            if (hasData)
            {
                logger.LogInformation("Data already exists");
                return;
            }

            var menus = new List<Menu>()
            {
                new Menu()
                {
                    Id = new Guid("c8b66e56-7a21-4166-98ac-ecefc3040a7f"),
                    Name = "Summer menu",
                    Categories = new List<Category>()
                    {

                    }
                },
                new Menu()
                {
                    Id = new Guid("14989654-7faf-4914-98d6-a4cf66ae0762"),
                    Name = "Winter menu",
                    Categories = new List<Category>()
                    {

                    }
                }
            };

            await context.InsertManyAsync(menus);
        }
    }
}
