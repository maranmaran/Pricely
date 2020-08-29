using MenuService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace MenuService.Domain.Seed
{
    public static class DbSeeder
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.SeedAllergens();
            builder.SeedCategories();
            builder.SeedIngredients();
            builder.SeedItems();
            builder.SeedMenus();
        }

        private static void SeedAllergens(this ModelBuilder builder)
        {
            var entities = new List<Allergen>()
            {
                new Allergen()
                {
                    Id = new Guid("4638a704-604e-4ebf-8da4-2271be9a953d"),
                    Name = "Gluten",
                    Description = "Found in bread."
                },
                new Allergen()
                {
                    Id = new Guid("625c60f6-75bf-421f-b681-6a3848ee25fe"),
                    Name = "Lactose",
                    Description = "Found in milk."
                },
                new Allergen()
                {
                    Id = new Guid("f5a6f6d4-ca85-4467-93bb-fabbdd633dfe"),
                    Name = "Orange",
                    Description = "Belongs to fruits food group."
                }
            };

            builder.Entity<Allergen>().HasData(entities);
        }
        private static void SeedIngredients(this ModelBuilder builder)
        {
            var entities = new List<Ingredient>()
            {
                new Ingredient()
                {
                    Id = new Guid("bf7905e0-6a86-44ec-a7c1-4e8e74e76ed5"),
                    Name = "Spaghetti",
                    Description = "Fine hand made spaghetti."
                },
                new Ingredient()
                {
                    Id = new Guid("40f44902-a38c-4617-a870-b7e6b917c31e"),
                    Name = "Tomato",
                    Description = "Best home grown tomato."
                },
                new Ingredient()
                {
                    Id = new Guid("c7205103-c499-4ec2-9a30-2d3e37d8b1ab"),
                    Name = "Tequilla",
                },
                new Ingredient()
                {
                    Id = new Guid("daff8847-a21b-45ec-a3d4-25b1c585d842"),
                    Name = "Juice",
                }
            };

            builder.Entity<Ingredient>().HasData(entities);
        }
        private static void SeedCategories(this ModelBuilder builder)
        {
            var entities = new List<Category>()
            {
                new Category()
                {
                    Id = new Guid("9a542d51-7aa0-488e-87f2-9aef980680cb"),
                    Name = "Food"
                },
                new Category()
                {
                    Id = new Guid("ce29cfd1-f374-4504-9358-a137ffa1c2c7"),
                    Name = "Non-alcoholic beverages"
                },
                new Category()
                {
                    Id = new Guid("42e774af-4372-4ccb-aee7-d8f7b11a9b28"),
                    Name = "Alcoholic beverages"
                }
            };

            builder.Entity<Category>().HasData(entities);
        }

        private static void SeedItems(this ModelBuilder builder)
        {
            var entities = new List<Item>()
            {
                new Item()
                {
                    Id = new Guid("d3b56d57-453c-4382-9e49-437022e47f2a"),
                    Name = "Tequilla sunrise",
                    Description = "Refreshing cocktail",
                    PictureUrl = "https://images.unsplash.com/photo-1558645836-e44122a743ee?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=634&q=80"
                },
                new Item()
                {
                    Id = new Guid("11c635ed-186e-4c2a-a7a7-1ef9e991d463"),
                    Name = "Coca-cola",
                    Description = "Worldwide drink",
                    PictureUrl = "https://images.unsplash.com/photo-1561758033-48d52648ae8b?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=634&q=80"
                },
                new Item()
                {
                    Id = new Guid("dbd5bd9c-f317-4b16-8e37-8b18bf07d614"),
                    Name = "Spaghetti bolognese",
                    Description = "Made in our kitchen",
                    PictureUrl = "https://images.unsplash.com/photo-1572441713132-c542fc4fe282?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=700&q=80"
                },
            };

            builder.Entity<Item>().HasData(entities);
        }


        private static void SeedMenus(this ModelBuilder builder)
        {

        }
    }
}
