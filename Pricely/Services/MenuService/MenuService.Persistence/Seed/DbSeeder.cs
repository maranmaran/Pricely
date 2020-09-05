using MenuService.Domain.Entities;
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
        private static ApplicationDbContext _context;

        public static async Task SeedAsync(DatabaseSettings settings, ILoggerFactory logFactory)
        {
            var logger = logFactory.CreateLogger<DbSeeder>();

            _context = new ApplicationDbContext(settings);

            try
            {
                var data = await _context.Menu.AsQueryable().ToListAsync();
                var hasData = data.Any();
                
                if (hasData == false)
                {
                    await SeedMenus();
                }
                else
                {
                    logger.LogInformation("Data already exists");
                }
            }
            catch (Exception e)
            {
                logger.LogError($"{e.Message} {e.InnerException?.Message}");
                throw;
            }
        }

        static async Task SeedMenus()
        {
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

            await _context.Menu.InsertManyAsync(menus);
        }
    }
}
