using DnsClient.Internal;
using MenuService.Domain.Entities;
using MenuService.Persistence;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MenuService.Domain.Seed
{
    public class DbSeeder
    {
        private static ApplicationDbContext _context;

        public static async Task SeedAsync(DatabaseSettings settings, ILoggerFactory loggerFactory)
        {
            _context = new ApplicationDbContext(settings);

            if (!_context.Menu.Database.GetCollection<Menu>(nameof(Menu)).AsQueryable().Any())
            {
                await SeedMenus();
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
