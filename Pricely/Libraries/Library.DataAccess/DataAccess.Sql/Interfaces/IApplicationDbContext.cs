using System.Threading;
using System.Threading.Tasks;
using DataAccess.Sql.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataAccess.Sql.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : EntityBase;
        EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : EntityBase;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
