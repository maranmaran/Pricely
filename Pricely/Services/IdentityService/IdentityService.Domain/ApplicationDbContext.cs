using DataAccess.Sql.Interfaces;
using DataAccess.Sql.Models;
using IdentityService.Domain.Entities;
using IdentityService.Domain.Seed;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace IdentityService.Domain
{
    public class ApplicationDbContext : IdentityDbContext<Company, Role, Guid>, IApplicationDbContext
    {

        public DbSet<Company> Companies { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed(); // seeds database with data..

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        public new DbSet<TEntity> Set<TEntity>() where TEntity : EntityBase
        {
            return base.Set<TEntity>();
        }

        public new EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : EntityBase
        {
            return base.Update(entity);
        }
    }
}
