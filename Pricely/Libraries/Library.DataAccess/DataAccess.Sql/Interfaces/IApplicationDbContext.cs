using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading;
using System.Threading.Tasks;
using DataAccess.Sql.Models;

namespace DataAccess.Sql.Interfaces
{
    public interface IApplicationDbContext
    {
        DatabaseFacade Database { get; }

        DbSet<TEntity> Set<TEntity>() where TEntity : EntityBase;
        EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : EntityBase;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
