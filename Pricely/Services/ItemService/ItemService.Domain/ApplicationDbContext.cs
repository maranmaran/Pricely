using DataAccess.Sql.Interfaces;
using ItemService.Domain.Entities;
using ItemService.Domain.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using EntityBase = DataAccess.Sql.Entities.EntityBase;

namespace ItemService.Domain
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Allergen> Allergens { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ItemAllergen> ItemAllergens { get; set; }
        public DbSet<ItemIngredient> ItemIngredients { get; set; }

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
