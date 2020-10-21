using DataAccess.Sql.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.Sql.Interfaces
{
    /// <summary>
    /// SQL Db context abstraction
    /// </summary>
    public interface IApplicationDbContext
    {
        DatabaseFacade Database { get; }

        DbSet<TEntity> Set<TEntity>() where TEntity : EntityBase;
        EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : EntityBase;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
