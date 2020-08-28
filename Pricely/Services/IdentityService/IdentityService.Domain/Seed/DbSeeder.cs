using IdentityService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace IdentityService.Domain.Seed
{
    public static class DbSeeder
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.SeedCompanies();
        }

        private static void SeedCompanies(this ModelBuilder builder)
        {
            var passwordHasher = new PasswordHasher<Company>();

            var companies = new List<Company>()
            {
                new Company()
                {
                    Id = new Guid("4638a704-604e-4ebf-8da4-2271be9a953d"),
                    Name = "Umba dumba bar",
                    Email = "umba.dumba@app.com",
                    LogoUrl = "https://unsplash.com/photos/3U9L9Chc3is"
                },
                new Company()
                {
                    Id = new Guid("4638a704-604e-4ebf-8da4-2271be9a953d"),
                    Name = "Ookla bar",
                    Email = "ookla@app.com",
                    LogoUrl = "https://unsplash.com/photos/CoNsEK5iHug"
                },

                new Company()
                {
                    Id = new Guid("4638a704-604e-4ebf-8da4-2271be9a953d"),
                    Name = "Fosta dosta bar",
                    Email = "fosta.dosta@app.com",
                    LogoUrl = "https://unsplash.com/photos/rXXT20z60f8"
                },
            };

            foreach (var company in companies)
            {
                company.PasswordHash = passwordHasher.HashPassword(company, "admin");
            }

            builder.Entity<Company>().HasData(companies);


            var addresses = new List<object>()
            {
                new
                {
                    CompanyId = new Guid(""),
                },

            };

            builder.Entity<Company>().OwnsOne(x => x.Address).HasData(addresses);
        }

    }
}
